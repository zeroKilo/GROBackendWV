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
using Be.Windows.Forms;

namespace GRPBackendWV
{
    public partial class PacketGenerator : Form
    {
        public List<string> protoNames = new List<string>();
        public List<int> protoIDs = new List<int>();

        public PacketGenerator()
        {
            InitializeComponent();
        }

        private void PacketGenerator_Load(object sender, EventArgs e)
        {
            protoNames.AddRange(Enum.GetNames(typeof(RMCP.PROTOCOL)));
            protoIDs.AddRange(Enum.GetValues(typeof(RMCP.PROTOCOL)).Cast<int>());
            while (true)
            {
                bool found = false;
                for (int i = 0; i < protoNames.Count - 1; i++)
                {
                    if (protoNames[i].CompareTo(protoNames[i + 1]) > 0)
                    {
                        found = true;
                        string tmp = protoNames[i];
                        protoNames[i] = protoNames[i + 1];
                        protoNames[i + 1] = tmp;
                        int tmp2 = protoIDs[i];
                        protoIDs[i] = protoIDs[i + 1];
                        protoIDs[i + 1] = tmp2;
                    }
                }
                if (!found)
                    break;
            }
            toolStripComboBox1.Items.Clear();
            for (int i = 0; i < protoNames.Count; i++)
                toolStripComboBox1.Items.Add(protoIDs[i].ToString("X2") + " - " + protoNames[i]);
            toolStripComboBox1.SelectedIndex = 0;
            hb1.ByteProvider = new DynamicByteProvider(new byte[4]);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            for (long i = 0; i < hb1.ByteProvider.Length; i++)
                m.WriteByte(hb1.ByteProvider.ReadByte(i));
            byte[] payload = m.ToArray();
            foreach (ClientInfo client in Global.clients)
            {
                QPacket q = new QPacket();
                q.m_oSourceVPort = new QPacket.VPort(0x31);
                q.m_oDestinationVPort = new QPacket.VPort(0x3f);
                q.type = QPacket.PACKETTYPE.DATA;
                q.flags = new List<QPacket.PACKETFLAG>();
                q.payload = new byte[0];
                q.uiSeqId = (ushort)(++client.seqCounter);
                q.m_bySessionID = client.sessionID;
                RMCP rmc = new RMCP();
                rmc.proto = (RMCP.PROTOCOL)protoIDs[toolStripComboBox1.SelectedIndex];
                rmc.methodID = Convert.ToUInt32(toolStripTextBox1.Text);
                rmc.callID = ++client.callCounter;
                RMCPCustom reply = new RMCPCustom();
                reply.buffer = payload;
                RMC.SendRequestPacket(client.udp, q, rmc, client, reply, true, 0);
            }
        }
    }
}
