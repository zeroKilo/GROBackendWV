using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class DS_ConnectionInfo
    {
        public byte m_bURLInitialized = 1;
        public string m_strStationURL1 = "prudp:/address=127.0.0.1;port=5004;RVCID=166202";
        public string m_strStationURL2 = "prudp:/address=127.0.0.1;port=5004;sid=15;type=2;RVCID=166202";
        public string m_strStationURL3 = "";
        public string m_strStationURL4 = "";
        public string m_strStationURL5 = "";
        public uint m_uiInputBandwidth;
        public uint m_uiInputLatency;
        public uint m_uiOutputBandwidth;
        public uint m_uiOutputLatency;

        public void toBuffer(Stream s)
        {
            s.WriteByte(m_bURLInitialized);
            Helper.WriteString(s, m_strStationURL1);
            Helper.WriteString(s, m_strStationURL2);
            Helper.WriteString(s, m_strStationURL3);
            Helper.WriteString(s, m_strStationURL4);
            Helper.WriteString(s, m_strStationURL5);
            Helper.WriteU32(s, m_uiInputBandwidth);
            Helper.WriteU32(s, m_uiInputLatency);
            Helper.WriteU32(s, m_uiOutputBandwidth);
            Helper.WriteU32(s, m_uiOutputLatency);
        }
    }
}
