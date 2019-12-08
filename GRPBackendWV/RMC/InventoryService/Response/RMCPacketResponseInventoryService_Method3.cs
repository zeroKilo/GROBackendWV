using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_Method3 : RMCPacketReply
    {
        public List<GR5_Consumable> cons = new List<GR5_Consumable>();

        public RMCPacketResponseInventoryService_Method3()
        {
            cons.Add(new GR5_Consumable());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)cons.Count);
            foreach (GR5_Consumable c in cons)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_Method3]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
