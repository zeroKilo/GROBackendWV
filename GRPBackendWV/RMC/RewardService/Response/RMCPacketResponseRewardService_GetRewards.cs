using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseRewardService_GetRewards : RMCPacketReply
    {
        public class Reward
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

        public List<Reward> rewards = new List<Reward>();

        public RMCPacketResponseRewardService_GetRewards()
        {
            rewards.Add(new Reward());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)rewards.Count);
            foreach (Reward r in rewards)
                r.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseRewardService_GetRewards]";
        }
    }
}
