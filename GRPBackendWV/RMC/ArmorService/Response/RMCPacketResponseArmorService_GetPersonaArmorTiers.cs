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
            public uint unk1;
            public byte unk2;
            public byte unk3;
            public byte unk4;
            public byte unk5;
            public byte unk6;
            public uint unk7;
            public uint unk8;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU8(s, unk4);
                Helper.WriteU8(s, unk5);
                Helper.WriteU8(s, unk6);
                Helper.WriteU32(s, unk7);
                Helper.WriteU32(s, unk8);
            }
        }

        public class ArmorInsert
        {
            public uint unk1;
            public byte unk2;
            public uint unk3;
            public uint unk4;
            public byte unk5;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU8(s, unk5);
            }
        }

        public class ArmorItem
        {
            public uint unk1;
            public byte unk2;
            public uint unk3;
            public uint unk4;
            public byte unk5;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU8(s, unk5);
            }
        }

        public List<ArmorTier> tiers = new List<ArmorTier>();
        public List<ArmorInsert> inserts = new List<ArmorInsert>();
        public List<ArmorItem> items = new List<ArmorItem>();

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
