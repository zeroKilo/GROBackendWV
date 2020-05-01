using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacket
    {
        public enum PROTOCOL
        {
            NATTraversalRelayProtocol = 3,
            GlobalNotificationEventProtocol = 0xE,
            MessageDeliveryProtocol = 0x1B,
            Authentication = 0xA,
            Secure = 0xB,
            Telemetry = 0x24,
            AMMGameClient = 0x65,
            PlayerProfileService = 0x67,
            ArmorService = 0x68,
            InventoryService = 0x69,
            LootService = 0x6A,
            WeaponService = 0x6B,
            FriendsService = 0x6C,
            ChatService = 0x6E,
            MissionService = 0x6F,
            PartyService = 0x70,
            StatisticsService = 0x72,
            AchievementsService = 0x73,
            ProgressionService = 0x74,
            DBGTelemetry = 0x75,
            RewardService = 0x76,
            StoreService = 0x77,
            AdvertisementsService = 0x79,
            SkillsService = 0x7A,
            Loadout = 0x7B,
            UnlockService = 0x7D,
            AvatarService = 0x7E,
            WeaponProficiencyService = 0x7F,
            OpsProtocolService = 0x80,
            ServerInfo = 0x82,
            LeaderboardService = 0x83,
            PveArchetypeService = 0x85,
            InboxMessageService = 0x86,
            ProfanityFilterService = 0x87,
            AbilityService = 0x89,
            SurveyService = 0x8B,
            OverlordNewsProtocol = 0x138A
        }

        public PROTOCOL proto;
        public bool isRequest;
        public uint callID;
        public uint methodID;
        public RMCPacketHeader header;

        public RMCPacket()
        {
        }

        public RMCPacket(QPacket p)
        {
            MemoryStream m = new MemoryStream(p.payload);
            Helper.ReadU32(m);
            ushort b = Helper.ReadU8(m);
            isRequest = (b >> 7) == 1;
            try
            {
                if ((b & 0x7F) != 0x7F)
                    proto = (PROTOCOL)(b & 0x7F);
                else
                {
                    b = Helper.ReadU16(m);
                    proto = (PROTOCOL)(b);
                }
            }
            catch
            {
                WriteLog(1, "Error: Unknown RMC packet protocol 0x" + b.ToString("X2"));
                return;
            }
            if (isRequest)
            {
                callID = Helper.ReadU32(m);
                methodID = Helper.ReadU32(m);
                switch (proto)
                {
                    case PROTOCOL.Authentication:
                        HandleAuthenticationMethods(m);
                        break;
                    case PROTOCOL.Secure:
                        HandleSecureMethods(m);
                        break;
                    case PROTOCOL.Telemetry:
                        HandleTelemetryMethods(m);
                        break;
                    case PROTOCOL.AMMGameClient:
                    case PROTOCOL.PlayerProfileService:
                    case PROTOCOL.ArmorService:
                    case PROTOCOL.InventoryService:
                    case PROTOCOL.LootService:
                    case PROTOCOL.WeaponService:
                    case PROTOCOL.FriendsService:
                    case PROTOCOL.ChatService:
                    case PROTOCOL.MissionService:
                    case PROTOCOL.PartyService:
                    case PROTOCOL.StatisticsService:
                    case PROTOCOL.AchievementsService:
                    case PROTOCOL.ProgressionService:
                    case PROTOCOL.DBGTelemetry:
                    case PROTOCOL.RewardService:
                    case PROTOCOL.StoreService:
                    case PROTOCOL.AdvertisementsService:
                    case PROTOCOL.SkillsService:
                    case PROTOCOL.Loadout:
                    case PROTOCOL.UnlockService:
                    case PROTOCOL.AvatarService:
                    case PROTOCOL.WeaponProficiencyService:
                    case PROTOCOL.OpsProtocolService:
                    case PROTOCOL.ServerInfo:
                    case PROTOCOL.LeaderboardService:
                    case PROTOCOL.PveArchetypeService:
                    case PROTOCOL.InboxMessageService:
                    case PROTOCOL.ProfanityFilterService:
                    case PROTOCOL.AbilityService:
                    case PROTOCOL.SurveyService:
                    case PROTOCOL.OverlordNewsProtocol:
                        break;
                    default:
                        WriteLog(1, "Error: No reader implemented for packet protocol " + proto);
                        break;
                }
            }
            else
            {
                WriteLog(1, "Got response for Protocol " + proto + " = " + (m.ReadByte() == 1 ? "Success" : "Fail"));
            }
        }

        private void HandleAuthenticationMethods(Stream s)
        {
            switch (methodID)
            {
                case 2:
                    header = new RMCPacketRequestLoginCustomData(s);
                    break;
                case 3:
                    header = new RMCPacketRequestRequestTicket(s);
                    break;
                default:
                    WriteLog(1, "Error: Unknown RMC Packet Authentication Method 0x" + methodID.ToString("X"));
                    break;
            }
        }

        private void HandleSecureMethods(Stream s)
        {
            switch (methodID)
            {
                case 4:
                    header = new RMCPacketRequestRegisterEx(s);
                    break;
                default:
                    WriteLog(1, "Error: Unknown RMC Packet Secure Method 0x" + methodID.ToString("X"));
                    break;
            }
        }

        private void HandleTelemetryMethods(Stream s)
        {
            switch (methodID)
            {
                case 1:
                    header = new RMCPacketRequestTelemetry_Method1(s);
                    break;
                default:
                    WriteLog(1, "Error: Unknown RMC Packet Method 0x" + methodID.ToString("X"));
                    break;
            }
        }

        public override string ToString()
        {
            return "[RMC Packet : Proto = " + proto + " CallID=" + callID + " MethodID=" + methodID + "]";
        }

        public string PayLoadToString()
        {
            StringBuilder sb = new StringBuilder();
            if (header != null)
                sb.Append(header);
            return sb.ToString();
        }

        public byte[] ToBuffer()
        {
            MemoryStream result = new MemoryStream();
            byte[] buff = header.ToBuffer();
            Helper.WriteU32(result, (uint)(buff.Length + 9));
            byte b = (byte)proto;
            if (isRequest)
                b |= 0x80;
            Helper.WriteU8(result, b);
            Helper.WriteU32(result, callID);
            Helper.WriteU32(result, methodID);
            result.Write(buff, 0, buff.Length);
            return result.ToArray();
        }

        private static void WriteLog(int priority, string s)
        {
            Log.WriteLine(priority, "[RMC Packet] " + s);
        }
    }
}
