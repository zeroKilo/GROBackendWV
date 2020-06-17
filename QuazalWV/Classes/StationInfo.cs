using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class StationInfo
    {
        public uint m_hObserver;
        public uint m_uiMachineUID;

        public StationInfo() { }
        public StationInfo(Stream s) 
        {
            m_hObserver = Helper.ReadU32(s);
            m_uiMachineUID = Helper.ReadU32(s);
        }

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_hObserver);
            Helper.WriteU32(s, m_uiMachineUID);
        }

        public string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "[StationInfo]");
            sb.AppendLine(tabs + " Observer   = 0x" + m_hObserver.ToString("X8"));
            sb.AppendLine(tabs + " MachineUID = 0x" + m_uiMachineUID.ToString("X8"));
            return sb.ToString();
        }
    }
}
