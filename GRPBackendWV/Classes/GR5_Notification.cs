using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Notification
    {
        public uint m_MajorType;
        public uint m_MinorType;
        public uint m_Param1;
        public uint m_Param2;
        public string m_String;
        public uint m_Param3;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_MajorType);
            Helper.WriteU32(s, m_MinorType);
            Helper.WriteU32(s, m_Param1);
            Helper.WriteU32(s, m_Param2);
            Helper.WriteString(s, m_String);
            Helper.WriteU32(s, m_Param3);
        }
    }
}
