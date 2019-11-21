using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_GetAllDefaultLoadoutKits : RMCPacketReply
    {
        public class LoadoutKit
        {
            public uint m_LoadoutKitID;
            public uint m_ClassID;
            public uint m_Weapon1ID;
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

        public List<LoadoutKit> kits = new List<LoadoutKit>();

        public RMCPacketResponseInventoryService_GetAllDefaultLoadoutKits()
        {
            for (int i = 0; i < 3; i++)
            {
                LoadoutKit kit = new LoadoutKit();
                kit.m_LoadoutKitID = kit.m_ClassID = (uint)i;
                kits.Add(kit);
            }
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)kits.Count);
            foreach (LoadoutKit kit in kits)
                kit.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_GetAllDefaultLoadoutKits]";
        }
    }
}
