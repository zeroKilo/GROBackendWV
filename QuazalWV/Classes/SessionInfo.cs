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
        public uint m_uiSessionID = 1;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_dohRootMulticastGroup);
            Helper.WriteString(s, sSessionName);
            Helper.WriteU32(s, m_uiSessionID);
        }
    }
}
