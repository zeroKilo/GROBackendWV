using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_GameSessionParticipant
    {
        public uint m_PId;
        public uint m_CgId;
        public uint m_SessionId;
        public uint m_TeamId;
        public uint m_Class;
        public uint m_Level;
        public uint m_GhostRank;
        public uint m_Status;
        public string msz_KeyName;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_PId);
            Helper.WriteU32(s, m_CgId);
            Helper.WriteU32(s, m_SessionId);
            Helper.WriteU32(s, m_TeamId);
            Helper.WriteU32(s, m_Class);
            Helper.WriteU32(s, m_Level);
            Helper.WriteU32(s, m_GhostRank);
            Helper.WriteU32(s, m_Status);
            Helper.WriteString(s, msz_KeyName);
        }
    }
}
