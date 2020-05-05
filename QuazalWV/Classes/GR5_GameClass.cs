using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_GameClass
    {
        public uint m_ID;
        public uint m_ModifierListID;
        public uint m_OasisID = 70870;
        public string m_Name;
        public List<uint> m_EquippableWeaponIDVector = new List<uint>();
        public List<uint> m_DefaultSkillNodeIDVector = new List<uint>();
        public uint m_LoadoutID;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_ModifierListID);
            Helper.WriteU32(s, m_OasisID);
            Helper.WriteString(s, m_Name);
            Helper.WriteU32(s, (uint)m_EquippableWeaponIDVector.Count);
            foreach (uint u in m_EquippableWeaponIDVector)
                Helper.WriteU32(s, u);
            Helper.WriteU32(s, (uint)m_DefaultSkillNodeIDVector.Count);
            foreach (uint u in m_DefaultSkillNodeIDVector)
                Helper.WriteU32(s, u);
            Helper.WriteU32(s, m_LoadoutID);
        }
    }
}
