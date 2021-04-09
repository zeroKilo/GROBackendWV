using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_Request
    {
        public uint m_MappedId;
        public uint m_CgId;
        public uint m_PId;
        public uint m_CId;
        public uint m_GameType;
        public uint m_TeamSize;
        public uint m_GhostRank;
        public uint m_State;
        public uint m_PendingDeletion;
        public uint m_Time;
        public uint m_SolutionId;
        public uint m_TeamId;
        public uint m_SessionId;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_MappedId);
            Helper.WriteU32(s, m_CgId);
            Helper.WriteU32(s, m_PId);
            Helper.WriteU32(s, m_CId);
            Helper.WriteU32(s, m_GameType);
            Helper.WriteU32(s, m_TeamSize);
            Helper.WriteU32(s, m_GhostRank);
            Helper.WriteU32(s, m_State);
            Helper.WriteU32(s, m_PendingDeletion);
            Helper.WriteU32(s, m_Time);
            Helper.WriteU32(s, m_SolutionId);
            Helper.WriteU32(s, m_TeamId);
            Helper.WriteU32(s, m_SessionId);
        }
    }
}
