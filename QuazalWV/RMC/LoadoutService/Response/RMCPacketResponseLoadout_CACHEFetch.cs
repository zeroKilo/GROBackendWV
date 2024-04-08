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
            var storeHeader = new GR5_RCHeader()
            {
                m_MethodId = 1,
                m_Checksum = 0x8E5C329A,
                m_Property = 0x77,
                m_Version = 1,
                m_OriginalSize = 40,
                m_Size = 40,
                
            };
            headers.Add(storeHeader);
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
