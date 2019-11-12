using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseChatService_Method14 : RMCPacketReply
    {
        public class BasicPersona
        {
            public uint unk1;
            public string unk2;
            public byte unk3;
            public uint unk4;
            public uint unk5;
            public uint unk6;
            public byte unk7;
            public byte unk8;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteString(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteU8(s, unk7);
                Helper.WriteU8(s, unk8);
            }
        }

        public List<BasicPersona> personas = new List<BasicPersona>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)personas.Count);
            foreach (BasicPersona p in personas)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseChatService_Method14]";
        }
    }
}
