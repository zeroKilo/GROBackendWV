using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace QuazalWV
{
    public static class Log
    {
        public static RichTextBox box = null;
        public static int MinPriority = 10; //1..10 1=less, 10=all
        public static string logFileName = "log.txt";
        public static string logPacketsFileName = "packetLog.bin";
        public static readonly object _sync = new object();
        public static readonly object _filesync = new object();
        public static StringBuilder logBuffer = new StringBuilder();
        public static List<byte[]> logPackets = new List<byte[]>();
        public static bool enablePacketLogging = true;

        public static void ClearLog()
        {
            if (File.Exists(logFileName))
                File.Delete(logFileName);
            if (File.Exists(logPacketsFileName))
                File.Delete(logPacketsFileName);
            lock (_sync)
            {
                logBuffer = new StringBuilder();
                logPackets = new List<byte[]>();
            }
        }

        public static void WriteLine(int priority, string s, object color = null)
        {
            if (box == null) return;
            try
            {
                box.Invoke(new Action(delegate
                {
                    string stamp = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " : [" + priority.ToString("D2") + "]";
                    if (priority <= MinPriority)
                    {
                        Color c;
                        if (color != null)
                            c = (Color)color;
                        else
                            c = Color.Black;
                        if (s.ToLower().Contains("error"))
                            c = Color.Red;
                        box.SelectionStart = box.TextLength;
                        box.SelectionLength = 0;
                        box.SelectionColor = c;
                        box.AppendText(stamp + s + "\n");
                        box.SelectionColor = box.ForeColor;
                        box.ScrollToCaret();                        
                    }
                    lock (_sync)
                    {
                        logBuffer.Append(stamp + s + "\n");
                        new Thread(tSaveLog).Start();
                    }
                }));
            }
            catch { }
        }

        public static string MakeDetailedPacketLog(byte[] data, bool isSinglePacket = false)
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                QPacket qp = new QPacket(data);
                sb.AppendLine("##########################################################");
                sb.AppendLine(qp.ToStringDetailed());
                if (qp.type == QPacket.PACKETTYPE.DATA && qp.m_byPartNumber == 0)
                {
                    switch (qp.m_oSourceVPort.type)
                    {
                        case QPacket.STREAMTYPE.OldRVSec:
                            if (qp.flags.Contains(QPacket.PACKETFLAG.FLAG_ACK))
                                break;
                            sb.AppendLine("Trying to process RMC packet...");
                            try
                            {
                                MemoryStream m = new MemoryStream(qp.payload);
                                RMCP p = new RMCP(qp);
                                m.Seek(p._afterProtocolOffset + 4, 0);
                                if (!p.isRequest)
                                    m.ReadByte();
                                p.methodID = Helper.ReadU32(m);
                                sb.AppendLine("\tRMC Request  : " + p.isRequest);
                                sb.AppendLine("\tRMC Protocol : " + p.proto);
                                sb.AppendLine("\tRMC Method   : " + p.methodID.ToString("X"));
                                if (p.proto == RMCP.PROTOCOL.GlobalNotificationEventProtocol && p.methodID == 1)
                                {
                                    sb.AppendLine("\t\tNotification :");
                                    sb.AppendLine("\t\t\tSource".PadRight(20) + ": 0x" + Helper.ReadU32(m).ToString("X8"));
                                    uint type = Helper.ReadU32(m);
                                    sb.AppendLine("\t\t\tType".PadRight(20) + ": " + (type / 1000));
                                    sb.AppendLine("\t\t\tSubType".PadRight(20) + ": " + (type % 1000));
                                    sb.AppendLine("\t\t\tParam 1".PadRight(20) + ": 0x" + Helper.ReadU32(m).ToString("X8"));
                                    sb.AppendLine("\t\t\tParam 2".PadRight(20) + ": 0x" + Helper.ReadU32(m).ToString("X8"));
                                    sb.AppendLine("\t\t\tParam String".PadRight(20) + ": " + Helper.ReadString(m));
                                    sb.AppendLine("\t\t\tParam 3".PadRight(20) + ": 0x" + Helper.ReadU32(m).ToString("X8"));
                                }
                                sb.AppendLine();
                            }
                            catch
                            {
                                sb.AppendLine("Error processing RMC packet");
                                sb.AppendLine();
                            }
                            break;
                        case QPacket.STREAMTYPE.DO:
                            if (qp.flags.Contains(QPacket.PACKETFLAG.FLAG_ACK))
                                break;
                            sb.AppendLine("Trying to unpack DO messages...");
                            try
                            {
                                MemoryStream m = new MemoryStream(qp.payload);
                                uint size = Helper.ReadU32(m);
                                byte[] buff = new byte[size];
                                m.Read(buff, 0, (int)size);
                                DO.UnpackMessage(buff, 1, sb);
                                sb.AppendLine();
                            }
                            catch
                            {
                                sb.AppendLine("Error processing DO messages");
                                sb.AppendLine();
                            }
                            break;
                    }
                }
                int size2 = qp.toBuffer().Length;
                if (size2 == data.Length || isSinglePacket)
                    break;
                MemoryStream m2 = new MemoryStream(data);
                m2.Seek(size2, 0);
                size2 = (int)(m2.Length - m2.Position);
                if (size2 <= 8)
                    break;
                data = new byte[size2];
                m2.Write(data, 0, size2);
            }
            return sb.ToString();
        }

        public static void LogPacket(bool sent, byte[] data)
        {
            if (!enablePacketLogging)
                return;
            MemoryStream m = new MemoryStream();
            m.WriteByte(1);//version
            m.WriteByte((byte)(sent ? 1 : 0));
            Helper.WriteU32(m, (uint)data.Length);
            m.Write(data, 0, data.Length);
            lock (_sync)
            {
                logPackets.Add(m.ToArray());
            }
        }

        public static void tSaveLog(object obj)
        {
            lock (_filesync)
            {
                string buffer = null;
                lock (_sync)
                {
                    buffer = logBuffer.ToString();
                    logBuffer.Clear();
                }
                if(buffer != null && buffer.Length > 0)
                    File.AppendAllText(logFileName, buffer);
                byte[] packet = null;
                lock (_sync)
                {
                    if (logPackets.Count != 0)
                    {
                        packet = logPackets[0];
                        logPackets.RemoveAt(0);
                    }
                }
                if (packet != null)
                {
                    FileStream fs = new FileStream(logPacketsFileName, FileMode.Append, FileAccess.Write);
                    fs.Write(packet, 0, packet.Length);
                    fs.Flush();
                    fs.Close();
                }
                Thread.Sleep(1);
            }
        }
    }
}
