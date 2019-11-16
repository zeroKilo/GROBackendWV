using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseServerInfo_Method1 : RMCPacketReply
    {
        public class TimeInfo
        {
            public uint u32Year;
            public uint u32Month;
            public uint u32Day;
            public uint u32Hour;
            public uint u32Minute;
            public uint u32Second;
            public uint u32Weekday;
            public uint u32Yearday;
            public uint i32DST;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, u32Year);
                Helper.WriteU32(s, u32Month);
                Helper.WriteU32(s, u32Day);
                Helper.WriteU32(s, u32Hour);
                Helper.WriteU32(s, u32Minute);
                Helper.WriteU32(s, u32Second);
                Helper.WriteU32(s, u32Weekday);
                Helper.WriteU32(s, u32Yearday);
                Helper.WriteU32(s, i32DST);
            }
        }

        public class TimeZoneInfo
        {
            public string szTimeZone;
            public string szDstTimeZone;
            public uint i32Dst;
            public void toBuffer(Stream s)
            {
                Helper.WriteString(s, szTimeZone);
                Helper.WriteString(s, szDstTimeZone);
                Helper.WriteU32(s, i32Dst);
            }
        }

        public TimeInfo _localTime = new TimeInfo();
        public TimeInfo _gmTime = new TimeInfo();
        public TimeZoneInfo _timeZone = new TimeZoneInfo();

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
            return "[RMCPacketResponseServerInfo_Method1]";
        }
    }
}
