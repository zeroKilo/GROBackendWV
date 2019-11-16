using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseStoreService_GetSKUs : RMCPacketReply
    {
        public class SKUItem
        {
            public uint m_ItemID;
            public uint m_DurabilityValue;
            public uint m_DurabilityValue2;
            public uint m_OasisName;
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

        public class SKU
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
            public uint m_OasisName;
            public List<SKUItem> m_ItemVector = new List<SKUItem>();
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
                foreach (SKUItem i in m_ItemVector)
                    i.toBuffer(s);
            }
        }

        public List<SKU> skus = new List<SKU>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)skus.Count);
            foreach (SKU s in skus)
                s.toBuffer(m);
            return m.ToArray(); ;
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStoreService_GetSKUs]";
        }
    }
}
