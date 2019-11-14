using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAvatarService_Method1 : RMCPacketReply
    {
        public class AvatarDecorator
        {
            public uint unk1;
            public uint unk2;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
            }
        }

        public List<AvatarDecorator> decos = new List<AvatarDecorator>();
        public List<uint> unk1 = new List<uint>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)decos.Count);
            foreach (AvatarDecorator d in decos)
                d.toBuffer(m);
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (uint u in unk1)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAvatarService_Method1]";
        }
    }
}
