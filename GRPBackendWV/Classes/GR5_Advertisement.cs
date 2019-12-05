using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Advertisement
    {
        public uint m_ID;
        public uint m_StoreItemID;
        public uint m_AssetId;
        public byte m_Layout;
        public byte m_Action;
        public uint m_OasisName;
        public string m_Criteria;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_StoreItemID);
            Helper.WriteU32(s, m_AssetId);
            Helper.WriteU8(s, m_Layout);
            Helper.WriteU8(s, m_Action);
            Helper.WriteU32(s, m_OasisName);
            Helper.WriteString(s, m_Criteria);
        }
    }
}
