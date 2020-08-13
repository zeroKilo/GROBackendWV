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
            Log.WriteLine(2, "[DO] Handling DO_JoinRequestMessage...");
            SendConnectionRequest(client, sessionID);
            List<byte[]> msgs = new List<byte[]>();
            InitSession(client);
            DupObj clientStation = new DupObj(DupObjClass.Station, client.stationID, 1);
            Payload_Station ps = new Payload_Station();
            ps.connectionInfo.m_strStationURL1 = "prudp:/address=255.0.0.0;port=4";
            ps.stationState = STATIONSTATE.JoiningSession;
            clientStation.Payload = ps;
            DO_Session.DupObjs.Add(clientStation);
            msgs.Add(DO_JoinResponseMessage.Create(1, new DupObj(DupObjClass.Station, client.stationID, 1)));
            msgs.Add(DO_CreateAndPromoteDuplicaMessage.Create(client.callCounterDO_RMC++, clientStation, 2));
            msgs.Add(DO_MigrationMessage.Create(client.callCounterDO_RMC++, new DupObj(DupObjClass.Station, 1), new DupObj(DupObjClass.Station, client.stationID), new DupObj(DupObjClass.Station, client.stationID), 3, new List<uint>()));
            clientStation.Master.ID = client.stationID;
            return DO_BundleMessage.Create(client, msgs);
        }

        private static void InitSession(ClientInfo client)
        {
            DO_Session.ResetObjects();
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
