using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseRewardService_GetUserReward : RMCPResponse
    {
        public List<uint> userRewards = new List<uint>();

        public RMCPacketResponseRewardService_GetUserReward()
        {
            userRewards.Add(0);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)userRewards.Count);
            foreach (uint u in userRewards)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseRewardService_GetUserReward]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
