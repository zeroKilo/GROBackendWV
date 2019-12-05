using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAvatarService_Method2 : RMCPacketReply
    {
        public List<GR5_AvatarPortrait> portraits = new List<GR5_AvatarPortrait>();
        public List<uint> unk1 = new List<uint>();

        public RMCPacketResponseAvatarService_Method2()
        {
            portraits.Add(new GR5_AvatarPortrait());
            unk1.Add(0);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)portraits.Count);
            foreach (GR5_AvatarPortrait p in portraits)
                p.toBuffer(m);
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (uint u in unk1)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAvatarService_Method2]";
        }
    }
}
