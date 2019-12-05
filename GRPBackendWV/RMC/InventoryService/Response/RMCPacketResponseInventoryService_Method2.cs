using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_Method2 : RMCPacketReply
    {
        public List<GR5_Boost> boosts = new List<GR5_Boost>();

        public RMCPacketResponseInventoryService_Method2()
        {
            boosts.Add(new GR5_Boost());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)boosts.Count);
            foreach (GR5_Boost b in boosts)
                b.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_Method2]";
        }
    }
}
