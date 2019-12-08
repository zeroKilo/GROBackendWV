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
        public List<GR5_ProfaneWord> words = new List<GR5_ProfaneWord>();

        public RMCPacketResponseProfanityFilterService_GetAllProfaneWords()
        {
            words.Add(new GR5_ProfaneWord());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)words.Count);
            foreach (GR5_ProfaneWord w in words)
                w.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseProfanityFilterService_GetAllProfaneWords]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
