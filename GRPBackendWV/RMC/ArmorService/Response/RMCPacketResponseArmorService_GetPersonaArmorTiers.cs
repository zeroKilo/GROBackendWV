using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseArmorService_GetPersonaArmorTiers : RMCPacketReply
    {
        public class ArmorTier
        {
            public uint Id;
            public byte Type;
            public byte Tier;
            public byte ClassID;
            public byte UnlockLevel;
            public byte InsertSlots;
            public uint AssetKey;
            public uint ModifierListId;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, Id);
                Helper.WriteU8(s, Type);
                Helper.WriteU8(s, Tier);
                Helper.WriteU8(s, ClassID);
                Helper.WriteU8(s, UnlockLevel);
                Helper.WriteU8(s, InsertSlots);
                Helper.WriteU32(s, AssetKey);
                Helper.WriteU32(s, ModifierListId);
            }
        }

        public class ArmorInsert
        {
            public uint Id;
            public byte Type;
            public uint AssetKey;
            public uint ModifierListID;
            public byte CharacterID;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, Id);
                Helper.WriteU8(s, Type);
                Helper.WriteU32(s, AssetKey);
                Helper.WriteU32(s, ModifierListID);
                Helper.WriteU8(s, CharacterID);
            }
        }

        public class ArmorItem
        {
            public uint Id;
            public byte Type;
            public uint AssetKey;
            public uint ModifierListID;
            public byte CharacterID;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, Id);
                Helper.WriteU8(s, Type);
                Helper.WriteU32(s, AssetKey);
                Helper.WriteU32(s, ModifierListID);
                Helper.WriteU8(s, CharacterID);
            }
        }

        public List<ArmorTier> tiers = new List<ArmorTier>();
        public List<ArmorInsert> inserts = new List<ArmorInsert>();
        public List<ArmorItem> items = new List<ArmorItem>();

        public RMCPacketResponseArmorService_GetPersonaArmorTiers()
        {
            tiers.Add(new ArmorTier());
            inserts.Add(new ArmorInsert());
            items.Add(new ArmorItem());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)tiers.Count);
            foreach (ArmorTier t in tiers)
                t.toBuffer(m);
            Helper.WriteU32(m, (uint)inserts.Count);
            foreach (ArmorInsert i in inserts)
                i.toBuffer(m);
            Helper.WriteU32(m, (uint)items.Count);
            foreach (ArmorItem i in items)
                i.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseArmorService_GetPersonaArmorTiers]";
        }
    }
}
