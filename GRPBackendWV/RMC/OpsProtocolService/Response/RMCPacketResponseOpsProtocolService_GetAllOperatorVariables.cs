using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseOpsProtocolService_GetAllOperatorVariables : RMCPacketReply
    {
        public class OperatorVariable
        {
            public uint unk1;
            public string unk2;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteString(s, unk2);
            }
        }

        public List<OperatorVariable> ops = new List<OperatorVariable>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)ops.Count);
            foreach (OperatorVariable v in ops)
                v.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketTesponseOpsProtocolService_GetAllOperatorVariables]";
        }
    }
}
