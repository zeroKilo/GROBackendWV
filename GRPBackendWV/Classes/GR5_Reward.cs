using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Reward
    {
        public uint mRewardID;
        public uint mRewardItem;
        public byte mRewardType;
        public byte mIsUniqueReward;
        public uint mAchievementID;
        public uint mAchievementGroupID;
        public uint mAchievementPoints;
        public uint mClassID;
        public uint mClassLevel;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mRewardID);
            Helper.WriteU32(s, mRewardItem);
            Helper.WriteU8(s, mRewardType);
            Helper.WriteU8(s, mIsUniqueReward);
            Helper.WriteU32(s, mAchievementID);
            Helper.WriteU32(s, mAchievementGroupID);
            Helper.WriteU32(s, mAchievementPoints);
            Helper.WriteU32(s, mClassID);
            Helper.WriteU32(s, mClassLevel);
        }
    }
}
