using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_GetAllDefaultLoadoutKits : RMCPacketReply
    {
        public List<GR5_LoadoutKit> kits = new List<GR5_LoadoutKit>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)kits.Count);
            foreach (GR5_LoadoutKit kit in kits)
                kit.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_GetAllDefaultLoadoutKits]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
