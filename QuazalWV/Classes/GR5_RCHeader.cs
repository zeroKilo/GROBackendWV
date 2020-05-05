using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_RCHeader
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
}
