using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_Boost
    {
        public uint m_ItemID;
        public uint m_AssetKey = 0x11801302; //ammo boost
        public uint m_ModifierList;
        public uint m_Type;
        public string m_Name;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ItemID);
            Helper.WriteU32(s, m_AssetKey);
            Helper.WriteU32(s, m_ModifierList);
            Helper.WriteU32(s, m_Type);
            Helper.WriteString(s, m_Name);
        }
    }
}
