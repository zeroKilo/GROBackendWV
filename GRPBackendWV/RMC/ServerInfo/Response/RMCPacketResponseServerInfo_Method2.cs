using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseServerInfo_Method2 : RMCPacketReply
    {
        public uint[] unk1 = new uint[9];

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            foreach (uint u in unk1)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseServerInfo_Method2]";
        }
    }
}
