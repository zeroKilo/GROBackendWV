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
        public byte m_bURLInitialized = 0;
        public string m_strStationURL1 = "";
        public string m_strStationURL2 = "";
        public string m_strStationURL3 = "";
        public string m_strStationURL4 = "";
        public string m_strStationURL5 = "";
        public uint m_uiInputBandwidth;
        public uint m_uiInputLatency;
        public uint m_uiOutputBandwidth;
        public uint m_uiOutputLatency;

        public DS_ConnectionInfo() { }

        public DS_ConnectionInfo(Stream s)
        {
            m_bURLInitialized = Helper.ReadU8(s);
            m_strStationURL1 = Helper.ReadString(s);
            m_strStationURL2 = Helper.ReadString(s);
            m_strStationURL3 = Helper.ReadString(s);
            m_strStationURL4 = Helper.ReadString(s);
            m_strStationURL5 = Helper.ReadString(s);
            m_uiInputBandwidth = Helper.ReadU32(s);
            m_uiInputLatency = Helper.ReadU32(s);
            m_uiOutputBandwidth = Helper.ReadU32(s);
            m_uiOutputLatency = Helper.ReadU32(s);
        }

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

        public string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "[DS_ConnectionInfo]");
            sb.AppendLine(tabs + " URLInitialized   = " + m_bURLInitialized);
            sb.AppendLine(tabs + " Station URL 1    = " + m_strStationURL1);
            sb.AppendLine(tabs + " Station URL 2    = " + m_strStationURL2);
            sb.AppendLine(tabs + " Station URL 3    = " + m_strStationURL3);
            sb.AppendLine(tabs + " Station URL 4    = " + m_strStationURL4);
            sb.AppendLine(tabs + " Station URL 5    = " + m_strStationURL5);
            sb.AppendLine(tabs + " Input Bandwidth  = " + m_uiInputBandwidth);
            sb.AppendLine(tabs + " Input Latency    = " + m_uiInputLatency);
            sb.AppendLine(tabs + " Output Bandwidth = " + m_uiOutputBandwidth);
            sb.AppendLine(tabs + " Output Latency   = " + m_uiOutputLatency);
            return sb.ToString();
        }
    }
}
