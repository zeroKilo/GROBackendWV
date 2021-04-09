using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponsePveArchetypeService_GetAllPveArchetypes : RMCPResponse
    {
        public List<GR5_PveArchetype> archetypes = new List<GR5_PveArchetype>();

        public RMCPacketResponsePveArchetypeService_GetAllPveArchetypes()
        {
            archetypes.Add(new GR5_PveArchetype());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)archetypes.Count);
            foreach (GR5_PveArchetype t in archetypes)
                t.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePveArchetypeService_GetAllPveArchetypes]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
