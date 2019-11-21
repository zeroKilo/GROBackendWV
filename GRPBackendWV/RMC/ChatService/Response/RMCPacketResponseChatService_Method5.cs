using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseChatService_Method5 : RMCPacketReply
    {
        public class Gathering
        {
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public ushort unk4;
            public ushort unk5;
            public uint unk6;
            public uint unk7;
            public uint unk8;
            public uint unk9;
            public string unk10;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU16(s, unk4);
                Helper.WriteU16(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteU32(s, unk7);
                Helper.WriteU32(s, unk8);
                Helper.WriteU32(s, unk9);
                Helper.WriteString(s, unk10);
            }
        }
        public Gathering unk1 = new Gathering();
        public string unk2;
        public byte unk3;
        public byte unk4;
        public byte unk5;
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            unk1.toBuffer(m);
            Helper.WriteString(m, unk2);
            Helper.WriteU8(m, unk3);
            Helper.WriteU8(m, unk4);
            Helper.WriteU8(m, unk5);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseChatService_Method5]";
        }
    }
}
