using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseLoadout_Method3 : RMCPacketReply
    {
        public class RCHeader
        {
            public uint[] unk1 = new uint[6];
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
            }
        }

        public List<RCHeader> headers = new List<RCHeader>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)headers.Count);
            foreach (RCHeader h in headers)
                h.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLoadout_Method3]";
        }
    }
}
