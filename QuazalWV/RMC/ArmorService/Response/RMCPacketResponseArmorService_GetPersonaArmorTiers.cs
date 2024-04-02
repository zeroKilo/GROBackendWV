using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseArmorService_GetPersonaArmorTiers : RMCPResponse
    {
        public List<GR5_PersonaArmorTier> list = new List<GR5_PersonaArmorTier>();

        public RMCPacketResponseArmorService_GetPersonaArmorTiers(ClientInfo client, byte[] payload)
        {
            uint tierID = BitConverter.ToUInt32(payload, 0xD);//request is tierId vector, so this should be changed
            uint pID = BitConverter.ToUInt32(payload, 0x11);
            list = DBHelper.GetPersonaArmorTiers(client.PID, tierID);
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
