using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseLootService_GetLootAssetKeyMap : RMCPResponse
    {
        public class GR5_LootAssetKey
        {
            public uint unk1;
            public uint unk2;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk1);
            }
        }

        public List<GR5_LootAssetKey> list = new List<GR5_LootAssetKey>();

        public RMCPacketResponseLootService_GetLootAssetKeyMap()
        {
            list.Add(new GR5_LootAssetKey());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_LootAssetKey u in list)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLootService_GetLootAssetKeyMap]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
