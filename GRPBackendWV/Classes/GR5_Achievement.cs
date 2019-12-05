using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Achievement
    {
        public uint m_ID;
        public uint m_Category;
        public uint m_SubCategory;
        public uint m_Flags;
        public uint m_Level;
        public uint m_AchievementPoints;
        public uint m_Icon;
        public string m_Expression = "1";
        public uint m_OasisNameID;
        public uint m_OasisDescriptionID;
        public uint m_StartDate;
        public uint m_EndDate;
        public byte m_MinLevel;
        public byte m_MaxLevel;
        public byte m_CommandoRequired;
        public byte m_ReconRequired;
        public byte m_SpecialistRequired;
        public byte m_MinPartySize;
        public uint m_AchievementGroupID;
        public List<uint> m_RewardIDVector = new List<uint>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_Category);
            Helper.WriteU32(s, m_SubCategory);
            Helper.WriteU32(s, m_Flags);
            Helper.WriteU32(s, m_Level);
            Helper.WriteU32(s, m_AchievementPoints);
            Helper.WriteU32(s, m_Icon);
            Helper.WriteString(s, m_Expression);
            Helper.WriteU32(s, m_OasisNameID);
            Helper.WriteU32(s, m_OasisDescriptionID);
            Helper.WriteU32(s, m_StartDate);
            Helper.WriteU32(s, m_EndDate);
            Helper.WriteU8(s, m_MinLevel);
            Helper.WriteU8(s, m_MaxLevel);
            Helper.WriteU8(s, m_CommandoRequired);
            Helper.WriteU8(s, m_ReconRequired);
            Helper.WriteU8(s, m_SpecialistRequired);
            Helper.WriteU8(s, m_MinPartySize);
            Helper.WriteU32(s, m_AchievementGroupID);
            Helper.WriteU32(s, (uint)m_RewardIDVector.Count);
            foreach (uint u in m_RewardIDVector)
                Helper.WriteU32(s, u);
        }
    }
}
