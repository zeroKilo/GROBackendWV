using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseUnlockService_Method1 : RMCPacketReply
    {
        public override byte[] ToBuffer()
        {
            return new byte[0];
        }

        public override string ToString()
        {
            return "[RMCPacketResponseUnlockService_Method1]";
        }
    }
}
