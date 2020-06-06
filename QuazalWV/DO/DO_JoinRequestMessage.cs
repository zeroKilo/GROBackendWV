using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_JoinRequestMessage
    {
        public static byte[] HandleMessage(ClientInfo client, byte[] data, byte sessionID)
        {
            Log.WriteLine(1, "[DO] Handling DO_JoinRequestMessage...");
            SendConnectionRequest(client, sessionID);
            List<byte[]> msgs = new List<byte[]>();
            msgs.Add(DO_JoinResponseMessage.Create(1, Helper.MakeDupObj(DO.CLASS.DOC_Station, 2), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1)));
            msgs.Add(DO_CreateAndPromoteDuplicaMessage.Create(3, Helper.MakeDupObj(DO.CLASS.DOC_Station, 2), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new Payload_Station().Create()));
            msgs.Add(DO_MigrationMessage.Create(1, Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), Helper.MakeDupObj(DO.CLASS.DOC_Station, 2), Helper.MakeDupObj(DO.CLASS.DOC_Station, 2), 3));
            return DO_BundleMessage.Create(client, msgs);
        }

        private static void SendConnectionRequest(ClientInfo client, byte sessionID)
        {
            QPacket qp = new QPacket();
            qp.m_oSourceVPort = new QPacket.VPort(0x11);
            qp.m_oDestinationVPort = new QPacket.VPort(0x11);
            qp.type = QPacket.PACKETTYPE.SYN;
            qp.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK, QPacket.PACKETFLAG.FLAG_HAS_SIZE };
            qp.m_uiConnectionSignature = client.IDsend;
            qp.payload = new byte[0];
            DO.Send(qp, client);
            qp = new QPacket();
            qp.m_bySessionID = sessionID;
            qp.m_oSourceVPort = new QPacket.VPort(0x11);
            qp.m_oDestinationVPort = new QPacket.VPort(0x11);
            qp.type = QPacket.PACKETTYPE.CONNECT;
            qp.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_RELIABLE, QPacket.PACKETFLAG.FLAG_NEED_ACK, QPacket.PACKETFLAG.FLAG_HAS_SIZE };
            qp.m_uiSignature = client.IDsend;
            qp.m_uiConnectionSignature = client.IDrecv;
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, 8);
            Helper.WriteU32(m, Helper.MakeDupObj(DO.CLASS.DOC_Station, 1));
            Helper.WriteU32(m, Helper.MakeDupObj(DO.CLASS.DOC_Station, 2));
            DO.MakeAndSend(client, qp, m.ToArray());
        }
    }
}
