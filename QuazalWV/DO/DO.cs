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
            StringBuilder sb = new StringBuilder();
            UnpackMessage(data, 0, sb);
            Log.WriteLine(10, "[DO] Unpacking request...\n" + sb.ToString());
            byte[] replyPayload = ProcessMessage(client, p, data);
            p.m_uiSignature = client.IDsend;
            SendACK(p, client);
            if (replyPayload != null)
            {
                SendMessage(client, p, replyPayload);
                sb = new StringBuilder();
                UnpackMessage(replyPayload, 0, sb);
                Log.WriteLine(10, "[DO] Unpacking response...\n" + sb.ToString());
            }
        }

        public static byte[] ProcessMessage(ClientInfo client, QPacket p, byte[] data)
        {
            METHOD method = (METHOD)data[0];
            byte[] replyPayload = null;
            switch (method)
            {
                case METHOD.JoinRequest:
                    replyPayload = DO_JoinRequestMessage.HandleMessage(client, data, p.m_bySessionID);
                    break;
                case METHOD.GetParticipantsRequest:
                    client.seqCounterDO = 1;
                    client.callCounterDO_RMC = 1;
                    client.stationID = 2;
                    replyPayload = DO_GetParticipantsRequestMessage.HandleMessage(client, data);
                    break;
                case METHOD.FetchRequest:
                    replyPayload = DO_FetchRequestMessage.HandleMessage(client, data);
                    break;
                case METHOD.RMCCall:
                    replyPayload = DO_RMCRequestMessage.HandleMessage(client, data);
                    break;
                case METHOD.CallOutcome:
                    Log.WriteLine(1, "[DO] Received Called Outcome 0x" + BitConverter.ToUInt32(data, 3).ToString("X") + " for call ID 0x" + BitConverter.ToUInt16(data, 1).ToString("X"));
                    break;
                case METHOD.Update:
                    Log.WriteLine(1, "[DO] Received Update for 0x" + BitConverter.ToUInt32(data, 1).ToString("X") + " (" + new DupObj(BitConverter.ToUInt32(data, 1)).getDesc() + ")");
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
            Log.WriteLine(10, "[DO] send ACK packet");
            Send(np, client);
        }

        private static void SendMessage(ClientInfo client, QPacket p, byte[] data)
        {
            p.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_NEED_ACK , QPacket.PACKETFLAG.FLAG_RELIABLE, QPacket.PACKETFLAG.FLAG_HAS_SIZE};
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)data.Length);
            m.Write(data, 0, data.Length);
            m.WriteByte((byte)QPacket.MakeChecksum(m.ToArray(), 0));
            Log.WriteLine(10, "sending DO message packet");
            MakeAndSend(client, p, m.ToArray());
        }

        public static void MakeAndSend(ClientInfo client, QPacket np, byte[] data)
        {
            MemoryStream m = new MemoryStream(data);
            if (data.Length < 0x3C3)
            {
                np.uiSeqId = client.seqCounterDO++;
                np.payload = data;
                np.payloadSize = (ushort)np.payload.Length;
                Log.WriteLine(10, "[DO] sent packet");
                Send(np, client);
            }
            else
            {
                int pos = 0;
                m.Seek(0, 0);
                np.m_byPartNumber = 0;
                while (pos < data.Length)
                {
                    np.uiSeqId = client.seqCounterDO++;
                    bool isLast = false;
                    int len = 0x3C3;
                    if (len + pos >= data.Length)
                    {
                        len = data.Length - pos;
                        isLast = true;
                    }
                    if (!isLast)
                        np.m_byPartNumber++;
                    else
                        np.m_byPartNumber = 0;
                    byte[] buff = new byte[len];
                    m.Read(buff, 0, len);
                    np.payload = buff;
                    np.payloadSize = (ushort)np.payload.Length;
                    Send(np, client);
                    pos += len;
                }
                Log.WriteLine(10, "[DO] sent packets");
            }
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

        public static void UnpackMessage(byte[] data, int tabs, StringBuilder sb)
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
    }
}
