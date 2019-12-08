using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePlayerProfileService_LoadCharacterProfiles : RMCPacketReply
    {
        public uint PersonaID;
        public string Name;
        public uint PortraitID;
        public uint DecoratorID;
        public uint AvatarBackgroundColor;
        public uint GRCash;
        public uint IGC;
        public uint AchievementPoints;
        public byte LastUsedCharacterID;
        public uint MaxInventorySlot;
        public uint MaxScrapYardSlot;
        public uint GhostRank;
        public uint Flag;
        public List<GR5_Character> Characters = new List<GR5_Character>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, PersonaID);
            Helper.WriteString(m, Name);
            Helper.WriteU32(m, PortraitID);
            Helper.WriteU32(m, DecoratorID);
            Helper.WriteU32(m, AvatarBackgroundColor);
            Helper.WriteU32(m, GRCash);
            Helper.WriteU32(m, IGC);
            Helper.WriteU32(m, AchievementPoints);
            Helper.WriteU8(m, LastUsedCharacterID);
            Helper.WriteU32(m, MaxInventorySlot);
            Helper.WriteU32(m, MaxScrapYardSlot);
            Helper.WriteU32(m, GhostRank);
            Helper.WriteU32(m, Flag);
            Helper.WriteU32(m, (uint)Characters.Count);
            foreach (GR5_Character c in Characters)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_LoadCharacterProfiles]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
