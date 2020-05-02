using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePveArchetypeService_Method1 : RMCPResponse
    {
        public List<GR5_PveArchetype> types = new List<GR5_PveArchetype>();

        public RMCPacketResponsePveArchetypeService_Method1()
        {
            types.Add(new GR5_PveArchetype());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)types.Count);
            foreach (GR5_PveArchetype t in types)
                t.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePveArchetypeService_Method1]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
