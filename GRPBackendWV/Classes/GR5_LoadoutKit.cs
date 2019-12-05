using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_LoadoutKit
    {
        public uint m_LoadoutKitID;
        public uint m_ClassID;
        public uint m_Weapon1ID = 0x1000;
        public uint m_Weapon2ID;
        public uint m_Weapon3ID;
        public uint m_Item1ID;
        public uint m_Item2ID;
        public uint m_Item3ID;
        public uint m_PowerID;
        public uint m_HelmetID;
        public uint m_ArmorID;
        public uint m_OasisDesc;
        public uint m_Flag;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_LoadoutKitID);
            Helper.WriteU32(s, m_ClassID);
            Helper.WriteU32(s, m_Weapon1ID);
            Helper.WriteU32(s, m_Weapon2ID);
            Helper.WriteU32(s, m_Weapon3ID);
            Helper.WriteU32(s, m_Item1ID);
            Helper.WriteU32(s, m_Item2ID);
            Helper.WriteU32(s, m_Item3ID);
            Helper.WriteU32(s, m_PowerID);
            Helper.WriteU32(s, m_HelmetID);
            Helper.WriteU32(s, m_ArmorID);
            Helper.WriteU32(s, m_OasisDesc);
            Helper.WriteU32(s, m_Flag);
        }
    }
}
