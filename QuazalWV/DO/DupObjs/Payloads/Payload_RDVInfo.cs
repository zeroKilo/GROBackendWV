using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_RDVInfo : DupObjPayload
    {
        public uint unk1;
        public byte[] buffParams = new byte[256];
        public uint unk2;

        public Payload_RDVInfo()
        {
        }

        public override byte[] toBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, 1);
            Helper.WriteU32(m, unk1);
            m.Write(buffParams, 0, 256);
            Helper.WriteU32(m, unk2);
            return m.ToArray();
        }

        public override string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "Unknown 1     = " + unk1);
            sb.Append(tabs + "Parameter     =");
            foreach (byte b in buffParams)
                sb.Append(" " + b.ToString("X2"));
            sb.AppendLine();
            sb.AppendLine(tabs + "Unknown 2     = " + unk2);
            return sb.ToString();
        }
    }
}
