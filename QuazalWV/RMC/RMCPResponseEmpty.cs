using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPResponseEmpty : RMCPResponse
    {
        public override byte[] ToBuffer()
        {
            return new byte[0];
        }

        public override string ToString()
        {
            return "[RMCPacketResponseEmpty]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
