using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseLootService_GetLootPointQualityMap : RMCPResponse
    {
        public class GR5_LootPointQuality
        {
            public uint unk1;
            public uint unk2;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk1);
            }
        }

        public List<GR5_LootPointQuality> list = new List<GR5_LootPointQuality>();

        public RMCPacketResponseLootService_GetLootPointQualityMap()
        {
            list.Add(new GR5_LootPointQuality());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_LootPointQuality u in list)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLootService_GetLootPointQualityMap]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
