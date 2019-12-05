using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_SkillModifierList
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
}
