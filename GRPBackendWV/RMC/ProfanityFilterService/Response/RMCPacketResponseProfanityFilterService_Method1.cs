using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseProfanityFilterService_Method1 : RMCPacketReply
    {
        public class ProfaneWord
        {
            public uint unk1;
            public byte unk2;
            public string unk3;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteString(s, unk3);
            }
        }

        public List<ProfaneWord> words = new List<ProfaneWord>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)words.Count);
            foreach (ProfaneWord w in words)
                w.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseProfanityFilterService_Method1]";
        }
    }
}
