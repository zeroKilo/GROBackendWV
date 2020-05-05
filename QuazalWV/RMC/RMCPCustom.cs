using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPCustom : RMCPResponse
    {
        public byte[] buffer;

        public override byte[] ToBuffer()
        {
            return buffer;
        }

        public override string ToString()
        {
            return "[RMCPacketCustom]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
