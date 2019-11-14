using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePveArchetypeService_Method1 : RMCPacketReply
    {
        public class PveArchetype
        {
            public uint unk1;
            public uint unk2;
            public float unk3;
            public float unk4;
            public uint unk5;
            public uint unk6;
            public float unk7;
            public float unk8;
            public float unk9;
            public byte unk10;
            public float unk11;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteFloat(s, unk3);
                Helper.WriteFloat(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteFloat(s, unk7);
                Helper.WriteFloat(s, unk8);
                Helper.WriteFloat(s, unk9);
                Helper.WriteU8(s, unk10);
                Helper.WriteFloat(s, unk11);
            }
        }

        public List<PveArchetype> types = new List<PveArchetype>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)types.Count);
            foreach (PveArchetype t in types)
                t.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePveArchetypeService_Method1]";
        }
    }
}
