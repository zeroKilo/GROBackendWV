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

namespace GRPBackendWV
{
    public partial class SendNotification : Form
    {
        public SendNotification()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, Convert.ToUInt32(textBox1.Text));
            Helper.WriteU32(m, Convert.ToUInt32(textBox2.Text) * 1000 + Convert.ToUInt32(textBox3.Text));
            Helper.WriteU32(m, Convert.ToUInt32(textBox4.Text));
            Helper.WriteU32(m, Convert.ToUInt32(textBox5.Text));
            string s = textBox7.Text.Trim();
            if (!s.EndsWith("\0"))
                s += '\0';
            Helper.WriteU16(m, (ushort)s.Length);
            foreach (char c in s)
                m.WriteByte((byte)c);
            Helper.WriteU32(m, Convert.ToUInt32(textBox6.Text)); 
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
                RMCPacket rmc = new RMCPacket();
                rmc.proto = RMCPacket.PROTOCOL.GlobalNotificationEventProtocol;
                rmc.methodID =1;
                rmc.callID = ++client.callCounter;
                RMCPacketCustom reply = new RMCPacketCustom();
                reply.buffer = payload;
                RMC.SendCustomPacket(client.udp, q, rmc, client, reply, true, 0);
            }
        }
    }
}
