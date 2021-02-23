using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCP
    {
        public enum PROTOCOL
        {
            NATTraversalRelayProtocol = 3,            
            GlobalNotificationEventProtocol = 0xE,
            MatchMakingService = 0x15,
            MessageDeliveryProtocol = 0x1B,
            AuthenticationService = 0xA,
            SecureService = 0xB,
            TelemetryService = 0x24,
            AMMGameClientService = 0x65,
            AMMDedicatedServerService = 0x66,
            PlayerProfileService = 0x67,
            ArmorService = 0x68,
            InventoryService = 0x69,
            LootService = 0x6A,
            WeaponService = 0x6B,
            FriendsService = 0x6C,
            ChatService = 0x6E,
            MissionService = 0x6F,
            PartyService = 0x70,
            RegistrationService = 0x71,
            StatisticsService = 0x72,
            AchievementsService = 0x73,
            ProgressionService = 0x74,
            DBGTelemetryService = 0x75,
            RewardService = 0x76,
            StoreService = 0x77,
            AdvertisementsService = 0x79,
            SkillsService = 0x7A,
            LoadoutService = 0x7B,
            TrackingService = 0x7C,
            UnlockService = 0x7D,
            AvatarService = 0x7E,
            WeaponProficiencyService = 0x7F,
            OpsProtocolService = 0x80,
            ProfilerService = 0x81,
            ServerInfoService = 0x82,
            LeaderboardService = 0x83,
            PveArchetypeService = 0x85,
            InboxMessageService = 0x86,
            ProfanityFilterService = 0x87,
            InspectPlayerService = 0x88,
            AbilityService = 0x89,
            SurveyService = 0x8B,
            LeaderboardProtocolService = 0x1388,
            RPNEProtocolService = 0x1389,
            OverlordNewsProtocolService = 0x138A,
            OverlordCoreProtocolService = 0x138B,
            ExtraContentProtocolService = 0x138C,
            OverlordFriendsProtocolService = 0x138D,
            OverlordAwardsProtocolService = 0x138E,
            OverlordChallengeProtocolService = 0x138F,
            OverlordDareProtocolService = 0x1390
        }

        public PROTOCOL proto;
        public bool isRequest;
        public bool success;
        public uint error;
        public uint callID;
        public uint methodID;
        public RMCPRequest request;
        public int _afterProtocolOffset;

        public RMCP()
        {
        }

        public RMCP(QPacket p)
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
                Log.WriteLine(1, "[RMC Packet] Error: Unknown RMC packet protocol 0x" + b.ToString("X2"));
                return;
            }
            _afterProtocolOffset = (int)m.Position;
        }
        

        public override string ToString()
        {
            return "[RMC Packet : Proto = " + proto + " CallID=" + callID + " MethodID=" + methodID + "]";
        }

        public string PayLoadToString()
        {
            StringBuilder sb = new StringBuilder();
            if (request != null)
                sb.Append(request);
            return sb.ToString();
        }

        public byte[] ToBuffer()
        {
            MemoryStream result = new MemoryStream();
            byte[] buff = request.ToBuffer();
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
    }
}
