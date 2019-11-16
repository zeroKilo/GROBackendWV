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
        public class Character
        {
            public uint PersonaID;
            public uint ClassID;
            public uint PEC;
            public uint Level;
            public uint UpgradePoints;
            public uint CurrentLevelPEC;
            public uint NextLevelPEC;
            public byte FaceID;
            public byte SkinToneID;
            public byte LoadoutKitID;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, PersonaID);
                Helper.WriteU32(s, ClassID);
                Helper.WriteU32(s, PEC);
                Helper.WriteU32(s, Level);
                Helper.WriteU32(s, UpgradePoints);
                Helper.WriteU32(s, CurrentLevelPEC);
                Helper.WriteU32(s, NextLevelPEC);
                Helper.WriteU8(s, FaceID);
                Helper.WriteU8(s, SkinToneID);
                Helper.WriteU8(s, LoadoutKitID);
            }
        }

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
        public List<Character> Characters = new List<Character>();

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
            foreach (Character c in Characters)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_LoadCharacterProfiles]";
        }
    }
}
