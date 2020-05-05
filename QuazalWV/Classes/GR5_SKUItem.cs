using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_SKUItem
    {
        public uint m_ItemID;
        public uint m_DurabilityValue;
        public uint m_DurabilityValue2;
        public uint m_OasisName = 70870;
        public float m_IGCPrice;
        public float m_GRCashPrice;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ItemID);
            Helper.WriteU32(s, m_DurabilityValue);
            Helper.WriteU32(s, m_DurabilityValue2);
            Helper.WriteU32(s, m_OasisName);
            Helper.WriteFloat(s, m_IGCPrice);
            Helper.WriteFloat(s, m_GRCashPrice);
        }
    }
}
