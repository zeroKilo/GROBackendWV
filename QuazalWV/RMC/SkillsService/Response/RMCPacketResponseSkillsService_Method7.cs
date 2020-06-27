using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseSkillsService_Method7 : RMCPResponse
    {
        public RMCPacketResponseSkillsService_Method7()
        {
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, 0);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_Method7]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
