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
        public List<GR5_Reward> rewards = new List<GR5_Reward>();

        public RMCPacketResponseRewardService_GetRewards()
        {
            rewards.Add(new GR5_Reward());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)rewards.Count);
            foreach (GR5_Reward r in rewards)
                r.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseRewardService_GetRewards]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
