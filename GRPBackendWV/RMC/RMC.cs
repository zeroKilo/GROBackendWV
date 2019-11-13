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
                case RMCPacket.PROTOCOL.Telemetry:
                    ProcessTelemetry(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.AMMGameClient:
                    ProcessAMMGameClient(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.PlayerProfileService:
                    ProcessPlayerProfileService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.ArmorService:
                    ProcessArmorService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.InventoryService:
                    ProcessInventoryService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.LootService:
                    ProcessLootService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.WeaponService:
                    ProcessWeaponService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.ChatService:
                    ProcessChatService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.MissionService:
                    ProcessMissionService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.PartyService:
                    ProcessPartyService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown73:
                    ProcessUnknown73(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.ProgressionService:
                    ProcessProgressionService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.RewardService:
                    ProcessRewardService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Unknown77:
                    ProcessUnknown77(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.SkillsService:
                    ProcessSkillsService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.Loadout:
                    ProcessLoadout(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.UnlockService:
                    ProcessUnlockService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.OpsProtocolService:
                    ProcessOpsProtocolService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.ServerInfo:
                    ProcessServerInfo(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.LeaderboardService:
                    ProcessLeaderboardService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.InboxMessageService:
                    ProcessInboxMessageService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.ProfanityFilterService:
                    ProcessProfanityFilterService(udp, p, rmc, client);
                    break;
                case RMCPacket.PROTOCOL.AbilityService:
                    ProcessAbilityService(udp, p, rmc, client);
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

        private static void ProcessTelemetry(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseTelemetry_TrackGameSession();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessPlayerProfileService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 18:
                    reply = new RMCPacketResponsePlayerProfileService_LoadCharacterProfiles();
                    ((RMCPacketResponsePlayerProfileService_LoadCharacterProfiles)reply).chars.Add(new RMCPacketResponsePlayerProfileService_LoadCharacterProfiles.Character());
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessArmorService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseArmorService_GetPersonaArmorTiers();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessInventoryService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseInventoryService_GetTemplateItems();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                case 4:
                    reply = new RMCPacketResponseInventoryService_GetAllApplyItems();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                case 16:
                    reply = new RMCPacketResponseInventoryService_GetAllDefaultLoadoutKits();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessLootService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseLootService_GetLootStatic();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessWeaponService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 3:
                    reply = new RMCPacketResponseWeaponService_GetTemplateWeaponMaps();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessChatService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 14:
                    reply = new RMCPacketResponseChatService_GetPlayerStatuses();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessMissionService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 4:
                    reply = new RMCPacketResponseMissionService_GetAllMissionTemplate();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessPartyService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponsePartyService_GetInviteeList();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown73(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 12:
                    reply = new RMCPacketResponseUnknown73_Method12();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessProgressionService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseProgressionService_GetLevels();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessRewardService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseRewardService_GetRewards();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnknown77(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnknown77_Method1();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessSkillsService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseSkillsService_GetGameClass();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                case 2:
                    reply = new RMCPacketResponseSkillsService_GetSkills();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessLoadout(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 3:
                    reply = new RMCPacketResponseLoadout_GetLoadoutPowers();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessUnlockService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseUnlockService_GetCurrentUserUnlock();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessOpsProtocolService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 19:
                    reply = new RMCPacketResponseOpsProtocolService_GetAllOperatorVariables();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                case 23:
                    reply = new RMCPacketResponseOpsProtocolService_GetAllPriorityBroadcasts();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessServerInfo(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseServerInfo_Method1();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                case 2:
                    reply = new RMCPacketResponseServerInfo_Method2();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                case 5:
                    reply = new RMCPacketResponseServerInfo_GetServerTime();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessLeaderboardService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseLeaderboardService_GetLeaderboards();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessInboxMessageService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseInboxMessageService_Method1();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessProfanityFilterService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseProfanityFilterService_GetAllProfaneWords();
                    SendReply(udp, p, rmc, client, reply);
                    break;
                default:
                    WriteLog("Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        private static void ProcessAbilityService(UdpClient udp, QPacket p, RMCPacket rmc, ClientInfo client)
        {
            RMCPacketReply reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseAbilityService_GetPersonaAbilityUpgrades();
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
