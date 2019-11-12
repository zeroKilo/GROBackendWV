using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePlayerProfileService_Method18 : RMCPacketReply
    {
        public class UnknownClass
        {
            public uint unk1;
            public string unk2;
            public uint unk3;
            public uint unk4;
            public uint unk5;
            public uint unk6;
            public uint unk7;
            public uint unk8;
            public byte unk9;
            public uint unk10;
            public uint unk11;
            public uint unk12;
            public uint unk13;

            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteString(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteU32(s, unk7);
                Helper.WriteU32(s, unk8);
                Helper.WriteU8(s, unk9);
                Helper.WriteU32(s, unk10);
                Helper.WriteU32(s, unk11);
                Helper.WriteU32(s, unk12);
                Helper.WriteU32(s, unk13);
            }
        }

        public class Character
        {
            public uint[] unk1 = new uint[7];
            public byte[] unk2 = new byte[3];
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
                s.Write(unk2, 0, 3);
            }
        }

        public UnknownClass unk1 = new UnknownClass();
        public List<Character> chars = new List<Character>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            unk1.toBuffer(m);
            Helper.WriteU32(m, (uint)chars.Count);
            foreach (Character c in chars)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_Method18]";
        }
    }
}
