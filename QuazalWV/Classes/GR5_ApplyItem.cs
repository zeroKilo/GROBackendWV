using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_ApplyItem
    {
        public uint m_ItemID;
        public uint m_AssetKey;
        public uint m_ApplyItemType;
        public uint m_ItemType;
        public uint m_DurabilityType;
        public uint m_Value1;
        public uint m_Value2;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ItemID);
            Helper.WriteU32(s, m_AssetKey);
            Helper.WriteU32(s, m_ApplyItemType);
            Helper.WriteU32(s, m_ItemType);
            Helper.WriteU32(s, m_DurabilityType);
            Helper.WriteU32(s, m_Value1);
            Helper.WriteU32(s, m_Value2);
        }
    }
}
