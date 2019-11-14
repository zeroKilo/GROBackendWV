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
        public class LoadoutKit
        {
            public uint[] unk1 = new uint[13];
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
            }
        }

        public List<LoadoutKit> kits = new List<LoadoutKit>();

        public RMCPacketResponseInventoryService_GetAllDefaultLoadoutKits()
        {
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)kits.Count);
            foreach (LoadoutKit kit in kits)
                kit.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_GetAllDefaultLoadoutKits]";
        }
    }
}
