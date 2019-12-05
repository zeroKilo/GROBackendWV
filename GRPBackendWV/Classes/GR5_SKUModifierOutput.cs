using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_SKUModifierOutput
    {
        public uint m_Type;
        public uint m_Target;
        public uint m_Value;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_Type);
            Helper.WriteU32(s, m_Target);
            Helper.WriteU32(s, m_Value);
        }
    }
}
