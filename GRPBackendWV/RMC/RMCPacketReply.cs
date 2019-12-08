using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public abstract class RMCPacketReply
    {
        public abstract override string ToString();
        public abstract string PayloadToString();
        public abstract byte[] ToBuffer();
    }
}
