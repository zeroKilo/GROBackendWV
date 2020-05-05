using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_DesignerStatistics
    {
        public uint m_ID;
        public uint m_AggregationType;
        public uint m_Flags;
        public uint m_DefaultValue;
        public uint m_OasisNameId = 70870;
        public uint m_OasisDescriptionId = 70870;
        public string m_Expression = "1";
        public string m_Name;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_AggregationType);
            Helper.WriteU32(s, m_Flags);
            Helper.WriteU32(s, m_DefaultValue);
            Helper.WriteU32(s, m_OasisNameId);
            Helper.WriteU32(s, m_OasisDescriptionId);
            Helper.WriteString(s, m_Expression);
            Helper.WriteString(s, m_Name);
        }
    }
}
