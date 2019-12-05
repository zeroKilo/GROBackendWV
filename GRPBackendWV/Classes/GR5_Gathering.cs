using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Gathering
    {
        public uint m_idMyself;
        public uint m_pidOwner;
        public uint m_pidHost;
        public ushort m_uiMinParticipants;
        public ushort m_uiMaxParticipants;
        public uint m_uiParticipationPolicy;
        public uint m_uiPolicyArgument;
        public uint m_uiFlags;
        public uint m_uiState;
        public string m_strDescription = "";

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_idMyself);
            Helper.WriteU32(s, m_pidOwner);
            Helper.WriteU32(s, m_pidHost);
            Helper.WriteU16(s, m_uiMinParticipants);
            Helper.WriteU16(s, m_uiMaxParticipants);
            Helper.WriteU32(s, m_uiParticipationPolicy);
            Helper.WriteU32(s, m_uiPolicyArgument);
            Helper.WriteU32(s, m_uiFlags);
            Helper.WriteU32(s, m_uiState);
            Helper.WriteString(s, m_strDescription);
        }
    }
}
