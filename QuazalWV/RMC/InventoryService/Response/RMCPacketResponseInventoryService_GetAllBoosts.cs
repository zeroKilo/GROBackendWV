using System.IO;
using System.Collections.Generic;
using QuazalWV.DB;

namespace QuazalWV
{
    public class RMCPacketResponseInventoryService_GetAllBoosts : RMCPResponse
    {
        public List<GR5_Boost> Boosts { get; set; }

        public RMCPacketResponseInventoryService_GetAllBoosts()
        {
            Boosts = BoostModel.GetBoosts();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)Boosts.Count);
            foreach (GR5_Boost b in Boosts)
                b.ToBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[GetAllBoosts Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
