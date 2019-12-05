using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacktResponseAMM_Method4 : RMCPacketReply
    {
        public uint unk1;

        public RMCPacktResponseAMM_Method4()
        {
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, unk1);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAMM_Method4]";
        }
    }
}
