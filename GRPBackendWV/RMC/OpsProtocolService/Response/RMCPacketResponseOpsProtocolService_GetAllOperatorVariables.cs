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
        public List<GR5_OperatorVariable> ops = new List<GR5_OperatorVariable>();

        public RMCPacketResponseOpsProtocolService_GetAllOperatorVariables()
        {
            ops.Add(new GR5_OperatorVariable());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)ops.Count);
            foreach (GR5_OperatorVariable v in ops)
                v.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseOpsProtocolService_GetAllOperatorVariables]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
