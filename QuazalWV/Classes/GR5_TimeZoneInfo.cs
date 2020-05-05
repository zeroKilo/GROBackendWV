using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_TimeZoneInfo
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
}
