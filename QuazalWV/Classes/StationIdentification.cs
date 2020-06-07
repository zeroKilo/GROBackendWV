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

        public StationIdentification() { }
        public StationIdentification(Stream s) 
        {
            m_strIdentificationToken = Helper.ReadString(s);
            m_strProcessName = Helper.ReadString(s);
            m_uiProcessType = Helper.ReadU32(s);
            m_uiProductVersion = Helper.ReadU32(s);
        }

        public void toBuffer(Stream s)
        {
            Helper.WriteString(s, m_strIdentificationToken);
            Helper.WriteString(s, m_strProcessName);
            Helper.WriteU32(s, m_uiProcessType);
            Helper.WriteU32(s, m_uiProductVersion);
        }

        public string getDesc()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[StationIdentification]");
            sb.AppendLine(" Identification Token = " + m_strIdentificationToken);
            sb.AppendLine(" Process Name = " + m_strProcessName);
            sb.AppendLine(" Process Type = " + m_uiProcessType);
            sb.AppendLine(" Product Version = " + m_uiProductVersion);
            return sb.ToString();
        }
    }
}
