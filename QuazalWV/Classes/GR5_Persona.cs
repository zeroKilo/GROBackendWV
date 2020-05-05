using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_Persona
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
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, PersonaID);
            Helper.WriteString(s, Name);
            Helper.WriteU32(s, PortraitID);
            Helper.WriteU32(s, DecoratorID);
            Helper.WriteU32(s, AvatarBackgroundColor);
            Helper.WriteU32(s, GRCash);
            Helper.WriteU32(s, IGC);
            Helper.WriteU32(s, AchievementPoints);
            Helper.WriteU8(s, LastUsedCharacterID);
            Helper.WriteU32(s, MaxInventorySlot);
            Helper.WriteU32(s, MaxScrapYardSlot);
            Helper.WriteU32(s, GhostRank);
            Helper.WriteU32(s, Flag);
        }
    }
}
