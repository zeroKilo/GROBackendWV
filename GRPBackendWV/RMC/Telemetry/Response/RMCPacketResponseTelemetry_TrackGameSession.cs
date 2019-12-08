using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseTelemetry_TrackGameSession : RMCPacketReply
    {
        public uint unk1;
        public List<string> unk2 = new List<string>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, unk1);
            Helper.WriteStringList(m, unk2);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseTelemetry_TrackGameSession]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
