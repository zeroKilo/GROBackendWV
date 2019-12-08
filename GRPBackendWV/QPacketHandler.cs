using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public static class QPacketHandler
    {
        public static QPacket ProcessSYN(QPacket p, IPEndPoint ep, out ClientInfo client)
        {
            client = Global.GetClientByEndPoint(ep);
            if (client == null)
            {
                Log.WriteLine(2, "[QAZAL] Creating new client data...");
                client = new ClientInfo();
                client.ep = ep;
                client.IDrecv = Global.idCounter++;
                client.PID = Global.pidCounter++;
                Global.clients.Add(client);
            }
            QPacket reply = new QPacket();
            reply.m_oSourceVPort = p.m_oDestinationVPort;
            reply.m_oDestinationVPort = p.m_oSourceVPort;
            reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            reply.type = QPacket.PACKETTYPE.SYN;
            reply.m_bySessionID = p.m_bySessionID;
            reply.m_uiSignature = p.m_uiSignature;
            reply.uiSeqId = p.uiSeqId;
            reply.m_uiConnectionSignature = client.IDrecv;
            reply.payload = new byte[0];
            return reply;
        }

        public static QPacket ProcessCONNECT(ClientInfo client, QPacket p)
        {
            client.IDsend = p.m_uiConnectionSignature;
            QPacket reply = new QPacket();
            reply.m_oSourceVPort = p.m_oDestinationVPort;
            reply.m_oDestinationVPort = p.m_oSourceVPort;
            reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            reply.type = QPacket.PACKETTYPE.CONNECT;
            reply.m_bySessionID = p.m_bySessionID;
            reply.m_uiSignature = client.IDsend;
            reply.uiSeqId = p.uiSeqId;
            reply.m_uiConnectionSignature = client.IDrecv;
            if (p.payload != null && p.payload.Length > 0)
                reply.payload = MakeConnectPayload(client,p);
            else
                reply.payload = new byte[0];
            return reply;
        }

        private static byte[] MakeConnectPayload(ClientInfo client, QPacket p)
        {
            MemoryStream m = new MemoryStream(p.payload);
            uint size = Helper.ReadU32(m);
            byte[] buff = new byte[size];
            m.Read(buff, 0, (int)size);
            size = Helper.ReadU32(m) - 16;
            buff = new byte[size];
            m.Read(buff, 0, (int)size);
            buff = Helper.Decrypt(client.sessionKey, buff);
            m = new MemoryStream(buff);
            Helper.ReadU32(m);
            Helper.ReadU32(m);
            uint responseCode = Helper.ReadU32(m);
            Log.WriteLine(2, "[QAZAL] Got response code 0x" + responseCode.ToString("X8"));
            m = new MemoryStream();
            Helper.WriteU32(m, 4);
            Helper.WriteU32(m, responseCode + 1);
            return m.ToArray();
        }

        public static QPacket ProcessDISCONNECT(ClientInfo client, QPacket p)
        {
            QPacket reply = new QPacket();
            reply.m_oSourceVPort = p.m_oDestinationVPort;
            reply.m_oDestinationVPort = p.m_oSourceVPort;
            reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            reply.type = QPacket.PACKETTYPE.DISCONNECT;
            reply.m_bySessionID = p.m_bySessionID;
            reply.m_uiSignature = client.IDsend - 0x10000;
            reply.uiSeqId = p.uiSeqId;
            reply.payload = new byte[0];
            return reply;
        }

        public static QPacket ProcessPING(ClientInfo client, QPacket p)
        {
            QPacket reply = new QPacket();
            reply.m_oSourceVPort = p.m_oDestinationVPort;
            reply.m_oDestinationVPort = p.m_oSourceVPort;
            reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            reply.type = QPacket.PACKETTYPE.PING;
            reply.m_bySessionID = p.m_bySessionID;
            reply.m_uiSignature = client.IDsend;
            reply.uiSeqId = p.uiSeqId;
            reply.m_uiConnectionSignature = client.IDrecv;
            reply.payload = new byte[0];
            return reply;
        }
    }
}
