using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class SessionInfosParameter
    {
        public byte[] m_cSessionParameters = new byte[256];
        public bool m_bSessionParametersAreSet;
        public SessionInfosParameter() { }
        public SessionInfosParameter(Stream s) 
        {
            m_cSessionParameters = new byte[256];
            s.Read(m_cSessionParameters, 0, 256);
            m_bSessionParametersAreSet = Helper.ReadU8(s) == 1;
        }

        public void toBuffer(Stream s)
        {
            s.Write(m_cSessionParameters, 0, 256);
            s.WriteByte((byte)(m_bSessionParametersAreSet ? 1 : 0));
        }

        public string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "[SessionInfosParameter]");
            sb.Append(tabs + " SessionParameters       = " );
            foreach (byte b in m_cSessionParameters)
                sb.Append(b.ToString("X2") + " ");
            sb.AppendLine(tabs + " SessionParametersAreSet = " + m_bSessionParametersAreSet);
            return sb.ToString();
        }
    }
}
