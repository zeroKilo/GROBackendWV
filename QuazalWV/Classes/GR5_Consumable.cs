using System;
using System.IO;

namespace QuazalWV
{
    public class GR5_Consumable
    {
        public uint m_ItemID;
        public uint m_AssetKey;
        public uint m_Type;
        public uint m_Value1;
        public uint m_Value2;
        public string m_Name;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ItemID);
            Helper.WriteU32(s, m_AssetKey);
            Helper.WriteU32(s, m_Type);
            Helper.WriteU32(s, m_Value1);
            Helper.WriteU32(s, m_Value2);
            Helper.WriteString(s, m_Name);
        }
    }
}
