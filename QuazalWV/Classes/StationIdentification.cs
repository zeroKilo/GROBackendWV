using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class StationIdentification
    {
        public string m_strIdentificationToken = "";
        public string m_strProcessName = "";
        public uint m_uiProcessType;
        public uint m_uiProductVersion;

        public void toBuffer(Stream s)
        {
            Helper.WriteString(s, m_strIdentificationToken);
            Helper.WriteString(s, m_strProcessName);
            Helper.WriteU32(s, m_uiProcessType);
            Helper.WriteU32(s, m_uiProductVersion);
        }
    }
}
