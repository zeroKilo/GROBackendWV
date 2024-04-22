using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;

namespace QuazalWV
{
    public static class RMC
    {
        public const uint MaxRmcPayloadSize = 963;
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
            WriteLog(10, "Handling packet...");
            RMCP rmc = new RMCP(p);
            if (rmc.isRequest)
                HandleRequest(client, p, rmc);
            else
                HandleResponse(client, p, rmc);
        }

        public static void HandleResponse(ClientInfo client, QPacket p, RMCP rmc)
        {
            ProcessResponse(client, p, rmc);
            WriteLog(1, "Received Response : " + rmc.ToString());
        }

        public static void ProcessResponse(ClientInfo client, QPacket p, RMCP rmc)
        {
            MemoryStream m = new MemoryStream(p.payload);
            m.Seek(rmc._afterProtocolOffset, 0);
            rmc.success = m.ReadByte() == 1;
            if (rmc.success)
            {
                rmc.callID = Helper.ReadU32(m);
                rmc.methodID = Helper.ReadU32(m);
            }
            else
            {
                rmc.error = Helper.ReadU32(m);
                rmc.callID = Helper.ReadU32(m);
            }
            WriteLog(1, "Got response for Protocol " + rmc.proto + " = " + (rmc.success ? "Success" : "Fail"));
        }

        public static void HandleRequest(ClientInfo client, QPacket p, RMCP rmc)
        {
            ProcessRequest(client, p, rmc);
            if (rmc.callID > client.callCounterRMC)
                client.callCounterRMC = rmc.callID;
            WriteLog(1, "Received Request : " + rmc.ToString());
            string payload = rmc.PayLoadToString();
            if (payload != "")
                WriteLog(5, payload);
            switch (rmc.proto)
            {
                case RMCP.PROTOCOL.AuthenticationService:
                    AuthenticationService.HandleAuthenticationServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.SecureService:
                    SecureService.HandleSecureServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.TelemetryService:
                    TelemetryService.HandleTelemetryServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.AMMGameClientService:
                    AMMGameClientService.HandleAMMGameClientRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.PlayerProfileService:
                    PlayerProfileService.HandlePlayerProfileServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.ArmorService:
                    ArmorService.HandleArmorServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.InventoryService:
                    InventoryService.HandleInventoryServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.LootService:
                    LootService.HandleLootServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.WeaponService:
                    WeaponService.HandleWeaponServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.FriendsService:
                    FriendsService.HandleFriendsServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.ChatService:
                    ChatService.HandleChatServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.MissionService:
                    MissionService.HandleMissionServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.PartyService:
                    PartyService.HandlePartyServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.RegistrationService:
                    WriteLog(1, "Error: No handler implemented for packet protocol RegistrationService");
                    break;
                case RMCP.PROTOCOL.StatisticsService:
                    StatisticsService.HandleStatisticsServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.AchievementsService:
                    AchievementsService.HandleAchievementsServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.ProgressionService:
                    ProgressionService.HandleProgressionServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.DBGTelemetryService:
                    DBGTelemetryService.HandleDBGTelemetryServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.RewardService:
                    RewardService.HandleRewardServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.StoreService:
                    StoreService.HandleStoreServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.AdvertisementsService:
                    AdvertisementsService.HandleAdvertisementsServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.SkillsService:
                    SkillService.HandleSkillsServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.LoadoutService:
                    LoadoutService.HandleLoadoutServiceLoadout(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.TrackingService:
                    WriteLog(1, "Error: No handler implemented for packet protocol TrackingService");
                    break;
                case RMCP.PROTOCOL.UnlockService:
                    UnlockService.HandleUnlockServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.AvatarService:
                    AvatarService.HandleAvatarServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.WeaponProficiencyService:
                    WeaponProficiencyService.HandleWeaponProficiencyServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.OpsProtocolService:
                    OpsProtocolService.HandleOpsProtocolServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.ProfilerService:
                    WriteLog(1, "Error: No handler implemented for packet protocol ProfilerService");
                    break;
                case RMCP.PROTOCOL.ServerInfoService:
                    ServerInfoService.HandleServerInfoRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.LeaderboardService:
                    LeaderboardService.HandleLeaderboardServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.PveArchetypeService:
                    PveArchetypeService.HandlePveArchetypeServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.InboxMessageService:
                    InboxMessageService.HandleInboxMessageServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.ProfanityFilterService:
                    ProfanityFilterService.HandleProfanityFilterServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.InspectPlayerService:
                    WriteLog(1, "Error: No handler implemented for packet protocol InspectPlayerService");
                    break;
                case RMCP.PROTOCOL.AbilityService:
                    AbilityService.HandleAbilityServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.SurveyService:
                    SurveyService.HandleSurveyServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.LeaderboardProtocolService:
                    WriteLog(1, "Error: No handler implemented for packet protocol LeaderboardProtocolService");
                    break;
                case RMCP.PROTOCOL.RPNEProtocolService:
                    WriteLog(1, "Error: No handler implemented for packet protocol RPNEProtocolService");
                    break;
                case RMCP.PROTOCOL.OverlordNewsProtocolService:
                    OverlordNewsProtocolService.HandleOverlordNewsProtocolRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.OverlordCoreProtocolService:
                    WriteLog(1, "Error: No handler implemented for packet protocol OverlordCoreProtocolService");
                    break;
                case RMCP.PROTOCOL.ExtraContentProtocolService:
                    WriteLog(1, "Error: No handler implemented for packet protocol ExtraContentProtocolService");
                    break;
                case RMCP.PROTOCOL.OverlordFriendsProtocolService:
                    WriteLog(1, "Error: No handler implemented for packet protocol OverlordFriendsProtocolService");
                    break;
                case RMCP.PROTOCOL.OverlordAwardsProtocolService:
                    WriteLog(1, "Error: No handler implemented for packet protocol OverlordAwardsProtocolService");
                    break;
                case RMCP.PROTOCOL.OverlordChallengeProtocolService:
                    WriteLog(1, "Error: No handler implemented for packet protocol OverlordChallengeProtocolService");
                    break;
                case RMCP.PROTOCOL.OverlordDareProtocolService:
                    WriteLog(1, "Error: No handler implemented for packet protocol OverlordDareProtocolService");
                    break;
                case RMCP.PROTOCOL.AMMDedicatedServerService:
                    AMMDedicatedServerService.HandleAMMDedicatedServerServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.MatchMakingService:
                    MatchMakingService.HandleMatchMakingServiceRequest(p, rmc, client);
                    break;
                case RMCP.PROTOCOL.NATTraversalRelayProtocol:
                    NATTraversalService.HandleNATTraversalServiceRequest(p, rmc, client);
                    break;
                default:
                    WriteLog(1, "Error: No handler implemented for packet protocol " + rmc.proto);
                    break;
            }
        }

        public static void ProcessRequest(ClientInfo client, QPacket p, RMCP rmc)
        {
            MemoryStream m = new MemoryStream(p.payload);
            m.Seek(rmc._afterProtocolOffset, 0);
            rmc.callID = Helper.ReadU32(m);
            rmc.methodID = Helper.ReadU32(m);
            switch (rmc.proto)
            {
                case RMCP.PROTOCOL.AuthenticationService:
                    AuthenticationService.ProcessAuthenticationServiceRequest(m, rmc);
                    break;
                case RMCP.PROTOCOL.SecureService:
                    SecureService.ProcessSecureServiceRequest(m, rmc);
                    break;
                case RMCP.PROTOCOL.TelemetryService:
                    TelemetryService.ProcessTelemetryServiceRequest(m, rmc);
                    break;
                case RMCP.PROTOCOL.PlayerProfileService:
                    PlayerProfileService.ProcessPlayerProfileServiceRequest(m, rmc);
                    break;
                case RMCP.PROTOCOL.FriendsService:
                    FriendsService.ProcessFriendsServiceRequest(m, rmc);
                    break;
                case RMCP.PROTOCOL.ChatService:
                    ChatService.ProcessChatServiceRequest(m, rmc);
                    break;
                case RMCP.PROTOCOL.PartyService:
                    PartyService.ProcessPartyServiceRequest(m, rmc);
                    break;
                case RMCP.PROTOCOL.StoreService:
                    StoreService.ProcessStoreServiceRequest(m, rmc);
                    break;
                case RMCP.PROTOCOL.InboxMessageService:
                    InboxMessageService.ProcessInboxMessageServiceRequest(m, rmc);
                    break;
                case RMCP.PROTOCOL.AMMGameClientService:
                case RMCP.PROTOCOL.ArmorService:
                case RMCP.PROTOCOL.InventoryService:
                case RMCP.PROTOCOL.LootService:
                case RMCP.PROTOCOL.WeaponService:
                case RMCP.PROTOCOL.MissionService:
                case RMCP.PROTOCOL.StatisticsService:
                case RMCP.PROTOCOL.AchievementsService:
                case RMCP.PROTOCOL.ProgressionService:
                case RMCP.PROTOCOL.DBGTelemetryService:
                case RMCP.PROTOCOL.RewardService:
                case RMCP.PROTOCOL.AdvertisementsService:
                case RMCP.PROTOCOL.SkillsService:
                case RMCP.PROTOCOL.LoadoutService:
                case RMCP.PROTOCOL.UnlockService:
                case RMCP.PROTOCOL.AvatarService:
                case RMCP.PROTOCOL.WeaponProficiencyService:
                case RMCP.PROTOCOL.OpsProtocolService:
                case RMCP.PROTOCOL.ServerInfoService:
                case RMCP.PROTOCOL.LeaderboardService:
                case RMCP.PROTOCOL.PveArchetypeService:
                case RMCP.PROTOCOL.ProfanityFilterService:
                case RMCP.PROTOCOL.AbilityService:
                case RMCP.PROTOCOL.SurveyService:
                case RMCP.PROTOCOL.OverlordNewsProtocolService:
                case RMCP.PROTOCOL.AMMDedicatedServerService:
                case RMCP.PROTOCOL.MatchMakingService:
                case RMCP.PROTOCOL.NATTraversalRelayProtocol:
                    break;
                default:
                    WriteLog(1, "Error: No request reader implemented for packet protocol " + rmc.proto);
                    break;
            }
        }


        public static void SendResponseWithACK(UdpClient udp, QPacket p, RMCP rmc, ClientInfo client, RMCPResponse reply, bool useCompression = true, uint error = 0)
        {
            WriteLog(2, "Response : " + reply.ToString());
            string payload = reply.PayloadToString();
            if (payload != "")
                WriteLog(5, "Response Data Content : \n" + payload);
            SendACK(udp, p, client);
            SendResponsePacket(udp, p, rmc, client, reply, useCompression, error);
        }

        private static void SendACK(UdpClient udp, QPacket p, ClientInfo client)
        {
            QPacket np = new QPacket(p.toBuffer())
            {
                flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK },
                m_oSourceVPort = p.m_oDestinationVPort,
                m_oDestinationVPort = p.m_oSourceVPort,
                m_uiSignature = client.IDsend,
                payload = new byte[0],
                payloadSize = 0
            };
            WriteLog(10, "send ACK packet");
            Send(udp, np, client);
        }

        private static void SendResponsePacket(UdpClient udp, QPacket p, RMCP rmc, ClientInfo client, RMCPResponse reply, bool useCompression, uint error)
        {
            MemoryStream m = new MemoryStream();
            if ((ushort)rmc.proto < 0x7F)
                Helper.WriteU8(m, (byte)rmc.proto);
            else
            {
                Helper.WriteU8(m, 0x7F);
                Helper.WriteU16(m, (ushort)rmc.proto);
            }
            byte[] buff;
            if (error == 0)
            {
                Helper.WriteU8(m, 0x1);
                Helper.WriteU32(m, rmc.callID);
                Helper.WriteU32(m, rmc.methodID | 0x8000);
                buff = reply.ToBuffer();
                if(buff != null) m.Write(buff, 0, buff.Length);                
            }
            else
            {
                Helper.WriteU8(m, 0);
                Helper.WriteU32(m, error);
                Helper.WriteU32(m, rmc.callID);
            } 
            buff = m.ToArray();
            m = new MemoryStream();
            Helper.WriteU32(m, (uint)buff.Length);
            m.Write(buff, 0, buff.Length);
            QPacket np = new QPacket(p.toBuffer())
            {
                flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_NEED_ACK },
                m_oSourceVPort = p.m_oDestinationVPort,
                m_oDestinationVPort = p.m_oSourceVPort,
                m_uiSignature = client.IDsend
            };
            MakeAndSend(client, np, m.ToArray());
        }

        public static void SendRequestPacket(UdpClient udp, QPacket p, RMCP rmc, ClientInfo client, RMCPResponse packet, bool useCompression, uint error)
        {
            MemoryStream m = new MemoryStream();
            if ((ushort)rmc.proto < 0x7F)
                Helper.WriteU8(m, (byte)((byte)rmc.proto | 0x80));
            else
            {
                Helper.WriteU8(m, 0xFF);
                Helper.WriteU16(m, (ushort)rmc.proto);
            }
            byte[] buff;
            if (error == 0)
            {
                Helper.WriteU32(m, rmc.callID);
                Helper.WriteU32(m, rmc.methodID);
                buff = packet.ToBuffer();
                m.Write(buff, 0, buff.Length);
            }
            else
            {
                Helper.WriteU32(m, error);
                Helper.WriteU32(m, rmc.callID);
            }
            buff = m.ToArray();
            m = new MemoryStream();
            Helper.WriteU32(m, (uint)buff.Length);
            m.Write(buff, 0, buff.Length);
            QPacket np = new QPacket(p.toBuffer())
            {
                flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_NEED_ACK },
                m_uiSignature = client.IDsend
            };
            MakeAndSend(client, np, m.ToArray());
        }

        public static void MakeAndSend(ClientInfo client, QPacket np, byte[] data)
        {
            MemoryStream m = new MemoryStream(data);
            if (data.Length < MaxRmcPayloadSize)
            {
                np.uiSeqId++;
                np.payload = data;
                np.payloadSize = (ushort)np.payload.Length;
                WriteLog(10, "sent packet");
                Send(client.udp, np, client);
            }
            else
            {
                np.flags.Add(QPacket.PACKETFLAG.FLAG_RELIABLE);
                int pos = 0;
                m.Seek(0, 0);
                np.m_byPartNumber = 0;
                while (pos < data.Length)
                {
                    np.uiSeqId = client.seqCounterReliable++;
                    bool isLast = false;
                    int len = (int)MaxRmcPayloadSize;
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
                    Send(client.udp, np, client);
                    pos += len;
                }
                WriteLog(10, "sent packets");
            }
        }

        public static void Send(UdpClient udp, QPacket p, ClientInfo client)
        {
            byte[] data = p.toBuffer();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");
            WriteLog(5, "send : " + p.ToStringShort());
            WriteLog(10, "send : " + sb.ToString());
            WriteLog(10, "send : " + p.ToStringDetailed());
            udp.Send(data, data.Length, client.ep);
            Log.LogPacket(true, data);
        }

        public static void SendNotification(ClientInfo client, uint source, uint type, uint subType, uint param1, uint param2, uint param3, string paramStr)
        {
            WriteLog(1, "Send Notification: [" + source.ToString("X8") + " " 
                                         + type.ToString("X8") + " "
                                         + subType.ToString("X8") + " " 
                                         + param1.ToString("X8") + " "
                                         + param2.ToString("X8") + " "
                                         + param3.ToString("X8") + " \""
                                         + paramStr + "\"]");
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, source);
            Helper.WriteU32(m, type * 1000 + subType);
            Helper.WriteU32(m, param1);
            Helper.WriteU32(m, param2);
            Helper.WriteU16(m, (ushort)(paramStr.Length + 1));
            foreach (char c in paramStr)
                m.WriteByte((byte)c);
            m.WriteByte(0);
            Helper.WriteU32(m, param3);
            byte[] payload = m.ToArray();
            QPacket q = new QPacket
            {
                m_oSourceVPort = new QPacket.VPort(0x31),
                m_oDestinationVPort = new QPacket.VPort(0x3f),
                type = QPacket.PACKETTYPE.DATA,
                flags = new List<QPacket.PACKETFLAG>(),
                payload = new byte[0],
                uiSeqId = ++client.seqCounter,
                m_bySessionID = client.sessionID
            };
            RMCP rmc = new RMCP
            {
                proto = RMCP.PROTOCOL.GlobalNotificationEventProtocol,
                methodID = 1,
                callID = ++client.callCounterRMC
            };
            RMCPCustom reply = new RMCPCustom();
            reply.buffer = payload;
            RMC.SendRequestPacket(client.udp, q, rmc, client, reply, true, 0);
        }

        private static void WriteLog(int priority, string s)
        {
            Log.WriteLine(priority, "[RMC] " + s);
        }

    }
}
