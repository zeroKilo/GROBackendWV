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
            METHOD proto = (METHOD)data[0];
            switch (proto)
            {
                case METHOD.GetParticipantsRequest:
                    DO_GetParticipantsRequest.HandlePacket(client, p);
                    break;
                default:
                    Log.WriteLine(1, "[DO] Error: Unknown Method 0x" + proto.ToString("X2"), Color.Red);
                    break;
            }
        }
    }
}
