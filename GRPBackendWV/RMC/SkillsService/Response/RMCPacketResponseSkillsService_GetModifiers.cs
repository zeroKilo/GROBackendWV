using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSkillsService_GetModifiers : RMCPacketReply
    {
        public class SkillModifier
        {
            public uint unk1;
            public byte unk2;
            public byte unk3;
            public byte unk4;
            public string unk5;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU8(s, unk4);
                Helper.WriteString(s, unk5);
            }
        }

        public List<SkillModifier> mods = new List<SkillModifier>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)mods.Count);
            foreach (SkillModifier skm in mods)
                skm.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetModifiers]";
        }
    }
}
