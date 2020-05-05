using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_AchievementGroup
    {
        public uint m_ID;
        public uint m_Category;
        public uint m_SubCategory;
        public uint m_Flags;
        public uint m_AchievementPoints;
        public uint m_Icon;
        public uint m_OasisNameID = 70870;
        public uint m_OasisDescriptionID = 70870;
        public uint m_ParentGroupID;
        public List<uint> m_AchievementIDVector = new List<uint>();
        public List<uint> m_RewardIDVector = new List<uint>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_Category);
            Helper.WriteU32(s, m_SubCategory);
            Helper.WriteU32(s, m_Flags);
            Helper.WriteU32(s, m_AchievementPoints);
            Helper.WriteU32(s, m_Icon);
            Helper.WriteU32(s, m_OasisNameID);
            Helper.WriteU32(s, m_OasisDescriptionID);
            Helper.WriteU32(s, m_ParentGroupID);
            Helper.WriteU32(s, (uint)m_AchievementIDVector.Count());
            foreach (uint u in m_AchievementIDVector)
                Helper.WriteU32(s, u);
            Helper.WriteU32(s, (uint)m_RewardIDVector.Count());
            foreach (uint u in m_RewardIDVector)
                Helper.WriteU32(s, u);
        }
    }
}
