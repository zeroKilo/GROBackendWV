using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseServerInfo_RequestServerInfo : RMCPResponse
    {
        public GR5_TimeInfo _localTime = new GR5_TimeInfo();
        public GR5_TimeInfo _gmTime = new GR5_TimeInfo();
        public GR5_TimeZoneInfo _timeZone = new GR5_TimeZoneInfo();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            _localTime.toBuffer(m);
            _gmTime.toBuffer(m);
            _timeZone.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseServerInfo_RequestServerInfo]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
