using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSkillsService_Method4 : RMCPacketReply
    {
        public class SkillModifierList
        {
            public uint unk1;
            public List<uint> unk2 = new List<uint>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, (uint)unk2.Count);
                foreach (uint u in unk2)
                    Helper.WriteU32(s, u);
            }
        }

        public List<SkillModifierList> sml = new List<SkillModifierList>();

        public RMCPacketResponseSkillsService_Method4()
        {
            sml.Add(new SkillModifierList());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)sml.Count);
            foreach (SkillModifierList s in sml)
                s.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_Method4]";
        }
    }
}
