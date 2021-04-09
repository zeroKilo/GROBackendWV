using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuazalWV
{
    public class GR5_ClusterQueue
    {
        public uint m_SolutionId;
        public uint m_ClusterId;
        public uint m_Priority;
        public uint m_GameType;
        public uint m_QueueTime;
        public uint m_TimeOut;
        public uint m_Expansion;
        public uint m_ServerPId;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_SolutionId);
            Helper.WriteU32(s, m_ClusterId);
            Helper.WriteU32(s, m_Priority);
            Helper.WriteU32(s, m_GameType);
            Helper.WriteU32(s, m_QueueTime);
            Helper.WriteU32(s, m_TimeOut);
            Helper.WriteU32(s, m_Expansion);
            Helper.WriteU32(s, m_ServerPId);
        }
    }
}
