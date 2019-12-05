using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_TemplateItem
    {
        public uint m_ItemID;
        public byte m_ItemType;
        public string m_ItemName;
        public byte m_DurabilityType;
        public bool m_IsInInventory;
        public bool m_IsSellable;
        public bool m_IsLootable;
        public bool m_IsRewardable;
        public bool m_IsUnlockable;
        public uint m_MaxItemInSlot;
        public uint m_GearScore;
        public float m_IGCValue;
        public uint m_OasisName;
        public uint m_OasisDesc;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ItemID);
            Helper.WriteU8(s, m_ItemType);
            Helper.WriteString(s, m_ItemName);
            Helper.WriteU8(s, m_DurabilityType);
            Helper.WriteBool(s, m_IsInInventory);
            Helper.WriteBool(s, m_IsSellable);
            Helper.WriteBool(s, m_IsLootable);
            Helper.WriteBool(s, m_IsRewardable);
            Helper.WriteBool(s, m_IsUnlockable);
            Helper.WriteU32(s, m_MaxItemInSlot);
            Helper.WriteU32(s, m_GearScore);
            Helper.WriteFloat(s, m_IGCValue);
            Helper.WriteU32(s, m_OasisName);
            Helper.WriteU32(s, m_OasisDesc);
        }
    }
}
