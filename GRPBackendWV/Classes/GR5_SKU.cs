using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_SKU
    {
        public uint m_ID;
        public uint m_Type;
        public uint m_AvailableStock;
        public uint m_TimeStart;
        public uint m_TimeExpired;
        public uint m_BuyIGCCost;
        public uint m_BuyGRCashCost;
        public uint m_AssetKey;
        public string m_Name;
        public uint m_OasisName = 70870;
        public List<GR5_SKUItem> m_ItemVector = new List<GR5_SKUItem>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_Type);
            Helper.WriteU32(s, m_AvailableStock);
            Helper.WriteU32(s, m_TimeStart);
            Helper.WriteU32(s, m_TimeExpired);
            Helper.WriteU32(s, m_BuyIGCCost);
            Helper.WriteU32(s, m_BuyGRCashCost);
            Helper.WriteU32(s, m_AssetKey);
            Helper.WriteString(s, m_Name);
            Helper.WriteU32(s, m_OasisName);
            Helper.WriteU32(s, (uint)m_ItemVector.Count);
            foreach (GR5_SKUItem i in m_ItemVector)
                i.toBuffer(s);
        }
    }
}
