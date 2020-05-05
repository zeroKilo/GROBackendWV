using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_AchievementCategory
    {
        public uint m_ID;
        public uint m_OasisNameID = 70870;
        public uint m_Category;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_OasisNameID);
            Helper.WriteU32(s, m_Category);
        }
    }
}
