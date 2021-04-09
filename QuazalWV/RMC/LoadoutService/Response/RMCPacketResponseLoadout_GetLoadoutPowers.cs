using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseLoadout_CACHEFetch : RMCPResponse
    {
        public List<GR5_RCHeader> headers = new List<GR5_RCHeader>();

        public RMCPacketResponseLoadout_CACHEFetch()
        {
            headers.Add(new GR5_RCHeader());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)headers.Count);
            foreach (GR5_RCHeader h in headers)
                h.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLoadout_CACHEFetch]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
