using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseRewardService_Method3 : RMCPacketReply
    {
        public List<GR5_RewardUserResult> unk1 = new List<GR5_RewardUserResult>();
        public List<GR5_UserItem> unk2 = new List<GR5_UserItem>();

        public RMCPacketResponseRewardService_Method3()
        {
            unk1.Add(new GR5_RewardUserResult());
            unk2.Add(new GR5_UserItem());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (GR5_RewardUserResult u in unk1)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)unk2.Count);
            foreach (GR5_UserItem u in unk2)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseRewardService_Method3]";
        }
    }
}
