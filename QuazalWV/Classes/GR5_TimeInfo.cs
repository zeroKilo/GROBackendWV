using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_TimeInfo
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
}
