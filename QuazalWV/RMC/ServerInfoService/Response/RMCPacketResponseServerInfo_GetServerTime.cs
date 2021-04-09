using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseServerInfo_GetServerUTCTime : RMCPResponse
    {
        public double unk1;

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteDouble(m, unk1);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseServerInfo_GetServerUTCTime]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
