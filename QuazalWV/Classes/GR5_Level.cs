using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_Level
    {
        public uint m_Id;
        public uint m_TotalPEC;
        public uint m_Level;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_Id);
            Helper.WriteU32(s, m_TotalPEC);
            Helper.WriteU32(s, m_Level);
        }
    }
}
