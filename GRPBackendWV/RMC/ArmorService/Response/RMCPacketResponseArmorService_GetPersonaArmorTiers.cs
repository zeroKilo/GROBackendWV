using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseArmorService_GetPersonaArmorTiers : RMCPResponse
    {
        public List<GR5_PersonaArmorTier> list = new List<GR5_PersonaArmorTier>();

        public RMCPacketResponseArmorService_GetPersonaArmorTiers(byte[] payload)
        {
            uint tierID = BitConverter.ToUInt32(payload, 0xD);
            uint pID = BitConverter.ToUInt32(payload, 0x11);
            list = DBHelper.GetPersonaArmorTiers(pID, tierID);
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
            return "[RMCPacketResponseArmorService_GetPersonaArmorTiers]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
