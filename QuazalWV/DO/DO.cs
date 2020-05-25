using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class DO
    {
        public enum METHOD
        {
            JoinRequest = 0x0,
            JoinResponse = 0x1,
            Update = 0x2,
            Delete = 0x4,
            Action = 0x5,
            CallOutcome = 0x8,
            RMCCall = 0xA,
            RMCResponse = 0xB,
            FetchRequest = 0xD,
            Bundle = 0xF,
            Migration = 0x11,
            CreateDuplicate = 0x12,
            CreateAndPromoteDuplicate = 0x13,
            GetParticipantsRequest = 0x14,
            GetParticipantsResponse = 0x15,
            NotHandledProtocol = 0xFE,
            EOS = 0xFF
        }

        public enum CLASS
        {
            DOC_DefaultCell = 07,
            DOC_SessionClock = 13,
            DOC_SES_cl_Player_NetZ = 19,
            DOC_SES_cl_RDVInfo_NetZ = 20,
            DOC_SES_cl_SessionInfos = 21,
            DOC_RootDO = 22,
            DOC_Station = 23,
            DOC_Session = 24,
            DOC_IDGenerator = 25,
            DOC_PromotionReferee = 26,
            DOC_NET_MessageBroker = 35,
        }

        public static void HandlePacket(UdpClient udp, QPacket p)
        {
            ClientInfo client = Global.GetClientByIDrecv(p.m_uiSignature);
            if (client == null)
                return;
            client.sessionID = p.m_bySessionID;
            if (p.uiSeqId > client.seqCounter)
                client.seqCounter = p.uiSeqId;
            client.udp = udp;
            if (p.flags.Contains(QPacket.PACKETFLAG.FLAG_ACK))
                return;
            Log.WriteLine(10, "[DO] Handling packet...");
            MemoryStream m = new MemoryStream(p.payload);
            uint packetSize = Helper.ReadU32(m);
            byte[] data = new byte[packetSize];
            m.Read(data, 0, (int)packetSize);
            byte[] replyPayload = ProcessMessage(client, p, data);
            p.m_uiSignature = client.IDsend;
            SendACK(p, client);
            if (replyPayload != null)
                SendMessage(client, p, replyPayload);
        }

        public static byte[] ProcessMessage(ClientInfo client, QPacket p, byte[] data)
        {
            METHOD method = (METHOD)data[0];
            byte[] replyPayload = null;
            switch (method)
            {
                case METHOD.JoinRequest:
                    replyPayload = DO_JoinRequestMessage.HandleMessage(client, data);
                    break;
                case METHOD.GetParticipantsRequest:
                    replyPayload = DO_GetParticipantsRequestMessage.HandleMessage(client, data);
                    break;
                case METHOD.FetchRequest:
                    replyPayload = DO_FetchRequestMessage.HandleMessage(client, data);
                    break;
                case METHOD.RMCCall:
                    replyPayload = DO_RMCRequestMessage.HandleMessage(client, data);
                    break;
                default:
                    Log.WriteLine(1, "[DO] Error: Unknown Method 0x" + data[0].ToString("X2") + " (" + method +")", Color.Red);
                    break;
            }
            return replyPayload;
        }


        private static void SendACK(QPacket p, ClientInfo client)
        {
            QPacket np = new QPacket(p.toBuffer());
            np.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            np.m_oSourceVPort = p.m_oDestinationVPort;
            np.m_oDestinationVPort = p.m_oSourceVPort;
            np.payload = new byte[0];
            np.payloadSize = 0;
            Log.WriteLine(10, "send ACK packet");
            Send(np, client);
        }

        private static void SendMessage(ClientInfo client, QPacket p, byte[] data)
        {
            p.uiSeqId++;
            p.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_NEED_ACK };
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)data.Length);
            m.Write(data, 0, data.Length);
            m.WriteByte((byte)QPacket.MakeChecksum(m.ToArray(), 0));
            p.payload = m.ToArray();
            p.payloadSize = (ushort)p.payload.Length;
            Log.WriteLine(10, "send DO message packet");
            Send(p, client);
        }


        public static void Send(QPacket p, ClientInfo client)
        {
            byte[] data = p.toBuffer();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");
            Log.WriteLine(5,  "[DO] send : " + p.ToStringShort());
            Log.WriteLine(10, "[DO] send : " + sb.ToString());
            Log.WriteLine(10, "[DO] send : " + p.ToStringDetailed());
            client.udp.Send(data, data.Length, client.ep);
        }
    }
}
