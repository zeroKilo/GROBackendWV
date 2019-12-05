using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Character
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
}
