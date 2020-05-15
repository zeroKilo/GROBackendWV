using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuazalWV;

namespace GRPBackendWV
{
    public partial class UDPProcessor : Form
    {
        public UDPProcessor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StringReader sr = new StringReader(rtb1.Text);
                string line;
                List<string> lines = new List<string>();
                while ((line = sr.ReadLine()) != null)
                    lines.Add(line);
                StringBuilder sb = new StringBuilder();
                foreach (string l in lines)
                {
                    try
                    {
                        byte[] data = makeArray(l.Trim());
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
                                        sb.AppendLine("Trying to unpack DO messages...");
                                        try
                                        {
                                            MemoryStream m = new MemoryStream(qp.payload);
                                            uint size = Helper.ReadU32(m);
                                            byte[] buff = new byte[size];
                                            m.Read(buff, 0, (int)size);
                                            UnpackMessage(buff, 1, sb);
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
                            if (size2 == data.Length)
                                break;
                            MemoryStream m2 = new MemoryStream(data);
                            m2.Seek(size2, 0);
                            size2 = (int)(m2.Length - m2.Position);
                            data = new byte[size2];
                            m2.Write(data, 0, size2);
                        }
                    }
                    catch
                    {
                        sb.AppendLine("Cant process: " + l);
                    }
                }
                rtb1.Text = sb.ToString();
            }
            catch { MessageBox.Show("Error"); }
        }

        private void UnpackMessage(byte[] data, int tabs, StringBuilder sb)
        {
            string t = "";
            for (int i = 0; i < tabs; i++)
                t += "\t";
            DO.METHOD method = (DO.METHOD)data[0];
            sb.AppendLine(t + " DO Message method\t: " + method);
            sb.Append(t + " DO Message data\t:");
            for (int i = 1; i < data.Length; i++)
                sb.Append(" " + data[i].ToString("X2"));
            sb.AppendLine();
            if (method == DO.METHOD.Bundle)
            {
                sb.AppendLine(t + " DO Sub Messages\t:");
                MemoryStream m = new MemoryStream(data);
                m.Seek(1, 0);
                while (true)
                {
                    uint size = Helper.ReadU32(m);
                    if (size == 0)
                        break;
                    byte[] buff = new byte[size];
                    m.Read(buff, 0, (int)size);
                    UnpackMessage(buff, tabs + 1, sb);
                }
            }
        }

        private byte[] makeArray(string s)
        {
            MemoryStream m = new MemoryStream();
            for (int i = 0; i < s.Length / 2; i++)
                m.WriteByte(Convert.ToByte(s.Substring(i * 2, 2), 16));
            return m.ToArray();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(QPacket.MakeChecksum(makeArray(toolStripTextBox1.Text.Trim().Replace(" ", ""))).ToString("X2"));
            }
            catch
            { }
        }
    }
}
