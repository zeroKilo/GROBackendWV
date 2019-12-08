using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseArmorService_Method2 : RMCPacketReply
    {
        public List<GR5_PersonaArmorTier> list = new List<GR5_PersonaArmorTier>();

        public RMCPacketResponseArmorService_Method2()
        {
            GR5_PersonaArmorTier p = new GR5_PersonaArmorTier();
            p.Inserts.Add(new GR5_ArmorInsertSlot());
            list.Add(p);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_PersonaArmorTier p in list)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseArmorService_Method2]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
