using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseRewardService_Method1 : RMCPacketReply
    {
        public class Reward
        {
            public uint unk1;
            public uint unk2;
            public byte unk3;
            public byte unk4;
            public uint unk5;
            public uint unk6;
            public uint unk7;
            public uint unk8;
            public uint unk9;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU8(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteU32(s, unk7);
                Helper.WriteU32(s, unk8);
                Helper.WriteU32(s, unk9);
            }
        }

        public List<Reward> rewards = new List<Reward>();

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
            return "[RMCPacketResponseRewardService_Method1]";
        }
    }
}
