using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseDBGTelemetry_DBGAMMClientInfo : RMCPResponse
    {
        public string unk1;

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteString(m, unk1);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseDBGTelemetry_DBGAMMClientInfo]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
