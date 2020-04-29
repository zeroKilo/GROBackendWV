using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketCustom : RMCPacketReply
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
