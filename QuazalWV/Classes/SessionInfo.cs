using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class SessionInfo
    {
        public uint m_dohRootMulticastGroup;
        public string sSessionName = "N01-GRO-DS034";
        public uint m_uiSessionID = DO_Session.ID;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_dohRootMulticastGroup);
            Helper.WriteString(s, sSessionName);
            Helper.WriteU32(s, m_uiSessionID);
        }

        public string getDesc()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[SessionInfo]");
            sb.AppendLine(" Root Multicast Group = 0x" + m_dohRootMulticastGroup.ToString("X8"));
            sb.AppendLine(" Session Name = " + sSessionName);
            sb.AppendLine(" Session ID = 0x" + m_uiSessionID.ToString("X8"));
            return sb.ToString();
        }
    }
}
