using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseLoadout_GetLoadoutPowers : RMCPacketReply
    {
        public class RCHeader
        {
            public uint m_MethodId;
            public uint m_Checksum;
            public uint m_Property;
            public uint m_Version;
            public uint m_Size;
            public uint m_OriginalSize;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_MethodId);
                Helper.WriteU32(s, m_Checksum);
                Helper.WriteU32(s, m_Property);
                Helper.WriteU32(s, m_Version);
                Helper.WriteU32(s, m_Size);
                Helper.WriteU32(s, m_OriginalSize);
            }
        }

        public List<RCHeader> headers = new List<RCHeader>();

        public RMCPacketResponseLoadout_GetLoadoutPowers()
        {
            headers.Add(new RCHeader());
        }

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
            return "[RMCPacketResponseLoadout_GetLoadoutPowers]";
        }
    }
}
