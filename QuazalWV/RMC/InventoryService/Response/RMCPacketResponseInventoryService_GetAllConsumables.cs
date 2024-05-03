using System;
using System.IO;
using System.Collections.Generic;

namespace QuazalWV
{
    public class RMCPacketResponseInventoryService_GetAllConsumables : RMCPResponse
    {
        public List<GR5_Consumable> Consumables { get; set; }

        public RMCPacketResponseInventoryService_GetAllConsumables()
        {
            Consumables = DB.ConsumableModel.GetConsumables();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)Consumables.Count);
            foreach (GR5_Consumable c in Consumables)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[GetAllConsumables Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
