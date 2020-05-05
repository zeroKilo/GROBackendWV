using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_InstancedStatistic
    {
        public uint m_ID;
        public uint m_Type;
        public uint m_TID;
        public uint m_Value;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_Type);
            Helper.WriteU32(s, m_TID);
            Helper.WriteU32(s, m_Value);
        }
    }
}
