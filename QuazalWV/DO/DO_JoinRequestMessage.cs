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
            msgs.Add(DO_JoinResponseMessage.Create(1, new DupObj(DupObjClass.Station, client.stationID, 1)));
            InitSession(client);
            msgs.Add(DO_CreateAndPromoteDuplicaMessage.Create(client.callCounterDO_RMC++, DO_Session.FindObj(new DupObj(DupObjClass.Station, client.stationID)), 2));
            DO_Session.FindObj(new DupObj(DupObjClass.Station, client.stationID)).Master.ID = client.stationID;
            msgs.Add(DO_MigrationMessage.Create(client.callCounterDO_RMC++, new DupObj(DupObjClass.Station, 1), new DupObj(DupObjClass.Station, client.stationID), new DupObj(DupObjClass.Station, client.stationID), 3));
            return DO_BundleMessage.Create(client, msgs);
        }

        private static void InitSession(ClientInfo client)
        {
            DO_Session.ResetObjects();
            Payload_Station ps = new Payload_Station();
            ps.connectionInfo.m_strStationURL1 = "";
            ps.connectionInfo.m_strStationURL2 = "";
            ps.stationState = 1;
            DO_Session.DupObjs.Add(new DupObj(DupObjClass.Station, client.stationID, 1, ps));
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
            Helper.WriteU32(m, new DupObj(DupObjClass.Station, 1));
            Helper.WriteU32(m, new DupObj(DupObjClass.Station, 2));
            DO.MakeAndSend(client, qp, m.ToArray());
        }
    }
}
