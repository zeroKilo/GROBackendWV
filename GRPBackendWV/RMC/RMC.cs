using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;

namespace GRPBackendWV
{
    public static class RMC
    {
        public static void HandlePacket(UdpClient udp, QPacket p)
        {
            ClientInfo client = Global.GetClientByIDrecv(p.m_uiSignature);
            if (client == null)
                return;
            if (p.flags.Contains(QPacket.PACKETFLAG.FLAG_ACK))
                return;
            WriteLog("Handling packet...");
            RMCPacket rmc = new RMCPacket(p);
            WriteLog("Received packet :\n" + rmc);
            switch (rmc.proto)
            {
                case RMCPacket.PROTOCOL.Authentication:
                    ProcessAuthentication(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Secure:
                    ProcessSecure(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown24:
                    ProcessUnknown24(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.AMMGameClient:
                    ProcessAMMGameClient(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown67:
                    ProcessUnknown67(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown69:
                    ProcessUnknown69(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown6A:
                    ProcessUnknown6A(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown6B:
                    ProcessUnknown6B(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown6E:
                    ProcessUnknown6E(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown6F:
                    ProcessUnknown6F(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown70:
                    ProcessUnknown70(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown74:
                    ProcessUnknown74(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown76:
                    ProcessUnknown76(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown7A:
                    ProcessUnknown7A(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown7B:
                    ProcessUnknown7B(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown7D:
                    ProcessUnknown7D(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown80:
                    ProcessUnknown80(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown82:
                    ProcessUnknown82(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown83:
                    ProcessUnknown83(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown86:
                    ProcessUnknown86(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown87:
                    ProcessUnknown87(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown89:
                    ProcessUnknown89(udp, p, rmc, client);
                    break;
                default:
                    WriteLog("No handler implemented for packet protocol " + rmc.proto);
                    break;
            }
        }

        private static void ProcessAMMGameClient(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 7:
                    reply = new RMCPacktResponseAMM_Method7();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessAuthentication(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 2:
                    RMCPacketRequestLoginCustomData h = (RMCPacketRequestLoginCustomData)rmc.header;
                    switch (h.className)
                    {
                        case "UbiAuthenticationLoginCustomData":
                            reply = new RMCPacketResponseLoginCustomData(client.PID);
                            client.sessionKey = ((RMCPacketResponseLoginCustomData)reply).ticket.sessionKey;
                            SendReply(udp, p, rmc, client, reply);
                            break;
                        default:
                            WriteLog("Error: Unknown RMC Packet Authentication Custom Data class " + h.className);
                            break;
                    }
                    break;
                case 3:
                    reply = new RMCPacketResponseRequestTicket(client.PID);
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown RMC Packet Authentication Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessSecure(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 4:
                    RMCPacketRequestRegisterEx h = (RMCPacketRequestRegisterEx)rmc.header;
                    switch (h.className)
                    {
                        case "UbiAuthenticationLoginCustomData":
                            reply = new RMCPacketResponseRegisterEx(client.PID);
                            SendReply(udp, p, rmc, client, reply);
                            break;
                        default:
                            WriteLog("Error: Unknown RMC Packet Secure Custom Data class " + h.className);
                            break;
                    }
                    break;
                default:
                    WriteLog("Error: Unknown RMC Packet Secure Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown24(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown24();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown67(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 18:
                    reply = new RMCPacketResponseUnknown67();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown69(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                case 4:
                case 16:
                    reply = new RMCPacketResponseUnknown69();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown6A(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown6A();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown6B(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 3:
                    reply = new RMCPacketResponseUnknown6B();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown6E(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 14:
                    reply = new RMCPacketResponseUnknown6E();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown6F(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 4:
                    reply = new RMCPacketResponseUnknown6F();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown70(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown70();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown74(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown74();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown76(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown76();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown7A(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown7A();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown7B(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 3:
                    reply = new RMCPacketResponseUnknown7B();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown7D(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown7D();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown80(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 19:
                case 23:
                    reply = new RMCPacketResponseUnknown80();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown82(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                case 2:
                case 5:
                    reply = new RMCPacketResponseUnknown82();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown83(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown83();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown86(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown86();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown87(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown87();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown89(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown89();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }


        private static void SendReply(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client, RMCPacketReply reply, bool useCompression = true)
        {
            SendACK(udp, p, client);
            SendReplyPacket(udp, p, rmc, client, reply, useCompression);
        }

        private static void SendACK(UdpClient udp, QPacket p, ClientInfo client)
        {
            QPacket np = new QPacket(p.toBuffer());
            np.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            np.m_oSourceVPort = p.m_oDestinationVPort;
            np.m_oDestinationVPort = p.m_oSourceVPort;
            np.m_uiSignature = client.IDsend;
            np.payload = new byte[0];
            np.payloadSize = 0;
            WriteLog("send ACK packet");
            Send(udp, np, client);
        }

        private static void SendReplyPacket(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client, RMCPacketReply reply, bool useCompression)
        {
            QPacket np = new QPacket(p.toBuffer());
            np.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_NEED_ACK };
            np.m_oSourceVPort = p.m_oDestinationVPort;
            np.m_oDestinationVPort = p.m_oSourceVPort;
            np.m_uiSignature = client.IDsend;
            np.uiSeqId++;
            MemoryStream m = new MemoryStream();
            if ((ushort)rmc.proto < 0x7F)
            {
                Helper.WriteU8(m, (byte)rmc.proto);
            }
            else
            {
                Helper.WriteU8(m, 0x7F);
                Helper.WriteU16(m, (ushort)rmc.proto);
            }
            Helper.WriteU8(m, 0x1);
            Helper.WriteU32(m, rmc.callID);
            Helper.WriteU32(m, rmc.methodID | 0x8000);
            byte[] buff = reply.ToBuffer();
            m.Write(buff, 0, buff.Length);
            buff = m.ToArray();
            m = new MemoryStream();
            Helper.WriteU32(m, (uint)buff.Length);
            m.Write(buff, 0, buff.Length);
            np.payload = m.ToArray();
            np.payloadSize = (ushort)np.payload.Length;
            WriteLog("send response packet");
            Send(udp, np, client);
            WriteLog("Response Data Content : \n" + reply.ToString());
        }

        public static void Send(UdpClient udp, QPacket p, ClientInfo client)
        {
            byte[] data = p.toBuffer();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");
            WriteLog("send : " + sb.ToString(), !Global.useDetailedLog);
            WriteLog("send : " + p.ToStringDetailed(), !Global.useDetailedLog);
            WriteLog("send : " + p.ToStringShort(), Global.useDetailedLog);
            udp.Send(data, data.Length, client.ep);
        }

        private static void WriteLog(string s, bool toFileOnly = false)
        {
            Log.WriteLine("[RMC] " + s, toFileOnly);
        }

    }
}
