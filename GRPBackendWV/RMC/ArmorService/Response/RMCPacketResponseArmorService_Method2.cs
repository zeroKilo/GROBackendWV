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
        public class ArmorInsertSlot
        {
            public uint InsertID;
            public uint Durability;
            public byte SlotID;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, InsertID);
                Helper.WriteU32(s, Durability);
                Helper.WriteU8(s, SlotID);
            }
        }

        public class PersonaArmorTier
        {
            public uint ArmorTierID;
            public List<ArmorInsertSlot> Inserts = new List<ArmorInsertSlot>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, ArmorTierID);
                Helper.WriteU32(s, (uint)Inserts.Count);
                foreach (ArmorInsertSlot a in Inserts)
                    a.toBuffer(s);
            }
        }

        public List<PersonaArmorTier> list = new List<PersonaArmorTier>();

        public RMCPacketResponseArmorService_Method2()
        {
            PersonaArmorTier p = new PersonaArmorTier();
            p.Inserts.Add(new ArmorInsertSlot());
            list.Add(p);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (PersonaArmorTier p in list)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseArmorService_Method2]";
        }
    }
}
