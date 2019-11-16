using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAchievementsService_Method9 : RMCPacketReply
    {
        public class Achievement
        {
            public uint m_ID;
            public uint m_Category;
            public uint m_SubCategory;
            public uint m_Flags;
            public uint m_Level;
            public uint m_AchievementPoints;
            public uint m_Icon;
            public string m_Expression;
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

        public class AchievementGroup
        {
            public uint m_ID;
            public uint m_Category;
            public uint m_SubCategory;
            public uint m_Flags;
            public uint m_AchievementPoints;
            public uint m_Icon;
            public uint m_OasisNameID;
            public uint m_OasisDescriptionID;
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

        public class AchievementCategory
        {
            public uint m_ID;
            public uint m_OasisNameID;
            public uint m_Category;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_ID);
                Helper.WriteU32(s, m_OasisNameID);
                Helper.WriteU32(s, m_Category);
            }
        }

        public List<Achievement> achs = new List<Achievement>();
        public List<AchievementGroup> groups = new List<AchievementGroup>();
        public List<AchievementCategory> cats = new List<AchievementCategory>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)achs.Count());
            foreach (Achievement a in achs)
                a.toBuffer(m);
            Helper.WriteU32(m, (uint)groups.Count());
            foreach (AchievementGroup g in groups)
                g.toBuffer(m);
            Helper.WriteU32(m, (uint)cats.Count());
            foreach (AchievementCategory c in cats)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAchievementsService_Method9]";
        }
    }
}
