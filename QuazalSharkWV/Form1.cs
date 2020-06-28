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
using Be.Windows.Forms;

namespace QuazalSharkWV
{
    public partial class Form1 : Form
    {
        public class LogEntry
        {
            public byte version;
            public bool sent;
            public byte[] raw;
            public QPacket packet;
        }

        public List<LogEntry> list = new List<LogEntry>();

        public Form1()
        {
            InitializeComponent();
        }

        private void loadPacketLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.bin|*.bin";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                list = new List<LogEntry>();
                MemoryStream m = new MemoryStream(File.ReadAllBytes(d.FileName));
                long size = m.Length;
                while (m.Position < size)
                {
                    LogEntry le = new LogEntry();
                    le.version = (byte)m.ReadByte();
                    le.sent = m.ReadByte() == 1;
                    int len = (int)Helper.ReadU32(m);
                    le.raw = new byte[len];
                    m.Read(le.raw, 0, len);
                    try
                    {
                        le.packet = new QPacket(le.raw);
                    }
                    catch 
                    {
                        le.packet = new QPacket(new byte[11]);
                    }
                    list.Add(le);
                }
                RefreshList();
            }
        }

        public void RefreshList()
        {
            listBox1.Items.Clear();
            foreach (LogEntry e in list)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(e.sent ? "---> " : "<--- ");
                    sb.Append("Size = " + e.raw.Length.ToString("X4") + " ");
                    sb.Append("Seq = " + e.packet.uiSeqId.ToString("X4") + " ");
                    sb.Append(e.packet.m_oSourceVPort.type + "\t");
                    sb.Append(e.packet.type + "\t");
                    sb.Append(e.packet.GetFlagsString());
                    if (e.packet.m_oSourceVPort.type == QPacket.STREAMTYPE.DO &&
                        e.packet.type == QPacket.PACKETTYPE.DATA &&
                        !e.packet.flags.Contains(QPacket.PACKETFLAG.FLAG_ACK))
                        sb.Append("(" + FindDOMethods(e.packet.payload) + ")");
                    if (e.sent &&
                        e.packet.type == QPacket.PACKETTYPE.DATA &&
                        e.packet.m_oSourceVPort.type == QPacket.STREAMTYPE.OldRVSec &&
                        !e.packet.flags.Contains(QPacket.PACKETFLAG.FLAG_ACK))
                        sb.Append("(" + GetRMCDetails(e.packet) + ")");
                    listBox1.Items.Add(sb.ToString());
                }
                catch (Exception ex)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(e.sent ? "---> " : "<--- ");
                    sb.Append("Size = " + e.raw.Length.ToString("X4") + " ");
                    sb.Append("Seq = " + e.packet.uiSeqId.ToString("X4") + " ");
                    sb.Append(e.packet.m_oSourceVPort.type + "\t");
                    sb.Append(e.packet.type + "\t");
                    sb.Append(e.packet.GetFlagsString());
                    listBox1.Items.Add(sb.ToString() + " Cant process!");
                }
            }
        }

        private string GetRMCDetails(QPacket q)
        {
            RMCP p = new RMCP(q);
            MemoryStream m = new MemoryStream(q.payload);
            m.Seek(p._afterProtocolOffset, 0);
            if (m.ReadByte() == 1)
                return p.proto + " " + Helper.ReadU32(m).ToString("X");
            else
                return p.proto + " fail";
        }

        private string FindDOMethods(byte[] data)
        {
            MemoryStream m = new MemoryStream(data);
            uint size = Helper.ReadU32(m);
            DO.METHOD method = (DO.METHOD)m.ReadByte();
            if (method != DO.METHOD.Bundle)
                return method.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(method);
            while (m.Position < data.Length)
            {
                size = Helper.ReadU32(m);
                if (size == 0)
                    break;
                method = (DO.METHOD)m.ReadByte();
                if ((int)method > 0x15)
                    throw new Exception();
                sb.Append("," + method);
                m.Seek(size - 1, SeekOrigin.Current);
            }
            return sb.ToString();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            if (n == -1)
                return;
            StringBuilder sb = new StringBuilder();
            sb.Append(Log.MakeDetailedPacketLog(list[n].raw, true));
            rtb1.Text = sb.ToString();
            hb1.ByteProvider = new DynamicByteProvider(list[n].raw);
        }
    }
}
