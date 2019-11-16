using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseProfanityFilterService_GetAllProfaneWords : RMCPacketReply
    {
        public class ProfaneWord
        {
            public uint mId;
            public byte mType;
            public string mWord;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, mId);
                Helper.WriteU8(s, mType);
                Helper.WriteString(s, mWord);
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
            return "[RMCPacketResponseProfanityFilterService_GetAllProfaneWords]";
        }
    }
}
