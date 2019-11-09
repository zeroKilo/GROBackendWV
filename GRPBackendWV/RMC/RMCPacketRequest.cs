using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public abstract class RMCPacketHeader
    {
        public abstract override string ToString();
        public abstract byte[] ToBuffer();
    }
}
