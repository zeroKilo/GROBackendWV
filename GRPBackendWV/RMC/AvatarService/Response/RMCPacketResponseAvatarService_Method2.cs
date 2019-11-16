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
        public class AvatarPortrait
        {
            public uint mItemID;
            public uint mPortraitID;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, mItemID);
                Helper.WriteU32(s, mPortraitID);
            }
        }

        public List<AvatarPortrait> portraits = new List<AvatarPortrait>();
        public List<uint> unk1 = new List<uint>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)portraits.Count);
            foreach (AvatarPortrait p in portraits)
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
