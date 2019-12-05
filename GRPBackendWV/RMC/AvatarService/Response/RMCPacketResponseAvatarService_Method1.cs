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
        public List<GR5_AvatarDecorator> decos = new List<GR5_AvatarDecorator>();
        public List<uint> unk1 = new List<uint>();

        public RMCPacketResponseAvatarService_Method1()
        {
            decos.Add(new GR5_AvatarDecorator());
            unk1.Add(0);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)decos.Count);
            foreach (GR5_AvatarDecorator d in decos)
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
