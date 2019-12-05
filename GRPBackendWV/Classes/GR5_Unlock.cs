using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Unlock
    {
        public uint mID;
        public uint mUnlockItem;
        public byte mUnlockType;
        public uint mClassID1;
        public uint mLevel1;
        public uint mClassID2;
        public uint mLevel2;
        public uint mClassID3;
        public uint mLevel3;
        public uint mAchievementID;
        public uint mAchievementWallID;
        public uint mFactionPoint1;
        public uint mFactionPoint2;
        public uint mFactionPoint3;
        public uint mFactionPoint4;
        public uint mFactionPoint5;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mID);
            Helper.WriteU32(s, mUnlockItem);
            Helper.WriteU8(s, mUnlockType);
            Helper.WriteU32(s, mClassID1);
            Helper.WriteU32(s, mLevel1);
            Helper.WriteU32(s, mClassID2);
            Helper.WriteU32(s, mLevel2);
            Helper.WriteU32(s, mClassID3);
            Helper.WriteU32(s, mLevel3);
            Helper.WriteU32(s, mAchievementID);
            Helper.WriteU32(s, mAchievementWallID);
            Helper.WriteU32(s, mFactionPoint1);
            Helper.WriteU32(s, mFactionPoint2);
            Helper.WriteU32(s, mFactionPoint3);
            Helper.WriteU32(s, mFactionPoint4);
            Helper.WriteU32(s, mFactionPoint5);
        }
    }
}
