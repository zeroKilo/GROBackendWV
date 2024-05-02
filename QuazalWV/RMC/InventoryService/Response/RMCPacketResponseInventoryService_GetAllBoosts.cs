using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseInventoryService_GetAllBoosts : RMCPResponse
    {
        public List<GR5_Boost> boosts = new List<GR5_Boost>();

        public RMCPacketResponseInventoryService_GetAllBoosts()
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
            return "[RMCPacketResponseInventoryService_GetAllBoosts]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
