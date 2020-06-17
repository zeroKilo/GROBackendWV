using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_IDRange : DupObjPayload
    {
        public uint min;
        public uint max;
        public Payload_IDRange(uint a, uint b)
        {
            min = a;
            max = b;
        }

        public override byte[] toBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, 1);
            Helper.WriteU32(m, min);
            Helper.WriteU32(m, max);
            return m.ToArray();
        }

        public override string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "Range Min = 0x" + min.ToString("X"));
            sb.AppendLine(tabs + "Range Max = 0x" + max.ToString("X"));
            return sb.ToString();
        }
    }
}
