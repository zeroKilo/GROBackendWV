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
        public class RewardUserResult
        {
            public uint unk1;
            public byte unk2;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
            }
        }

        public class UserItem
        {
            public uint unk1;
            public uint unk2;
            public byte unk3;
            public uint unk4;
            public uint unk5;
            public float unk6;
            public float unk7;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteFloat(s, unk6);
                Helper.WriteFloat(s, unk7);
            }
        }

        public List<RewardUserResult> unk1 = new List<RewardUserResult>();
        public List<UserItem> unk2 = new List<UserItem>();

        public RMCPacketResponseRewardService_Method3()
        {
            unk1.Add(new RewardUserResult());
            unk2.Add(new UserItem());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (RewardUserResult u in unk1)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)unk2.Count);
            foreach (UserItem u in unk2)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseRewardService_Method3]";
        }
    }
}
