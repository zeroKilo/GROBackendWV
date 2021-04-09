using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseRewardService_RewardUser : RMCPResponse
    {
        public List<GR5_RewardUserResult> rewardResults = new List<GR5_RewardUserResult>();
        public List<GR5_UserItem> rewardItems = new List<GR5_UserItem>();

        public RMCPacketResponseRewardService_RewardUser()
        {
            rewardResults.Add(new GR5_RewardUserResult());
            rewardItems.Add(new GR5_UserItem());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)rewardResults.Count);
            foreach (GR5_RewardUserResult u in rewardResults)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)rewardItems.Count);
            foreach (GR5_UserItem u in rewardItems)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseRewardService_Method3]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
