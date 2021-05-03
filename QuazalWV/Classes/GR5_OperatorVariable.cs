using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_OperatorVariable
    {
        public uint m_Id;
        public string m_Value;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_Id);
            Helper.WriteString(s, m_Value);
        }
    }
}
