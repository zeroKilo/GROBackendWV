using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuazalWV
{
    public class GR5_DedicatedServers
    {
        public uint m_ServerPId;
        public uint m_ClusterId;
        public string m_DServerURL;
        public string m_DServerName;
        public uint m_GameType;
        public uint m_Status;
        public uint m_SessionId;
        public uint m_SolutionId;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ServerPId);
            Helper.WriteU32(s, m_ClusterId);
            Helper.WriteString(s, m_DServerURL);
            Helper.WriteString(s, m_DServerName);
            Helper.WriteU32(s, m_GameType);
            Helper.WriteU32(s, m_Status);
            Helper.WriteU32(s, m_SessionId);
            Helper.WriteU32(s, m_SolutionId);
        }
    }
}
