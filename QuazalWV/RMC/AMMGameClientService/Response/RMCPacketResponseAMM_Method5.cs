using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAMM_Method5 : RMCPResponse
    {
        public uint unk = 1;

        public RMCPacketResponseAMM_Method5()
        {
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, unk);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAMM_Method5]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
