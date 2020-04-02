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
        public List<GR5_SkillModifier> mods = new List<GR5_SkillModifier>();

        public RMCPacketResponseSkillsService_GetModifiers()
        {
            mods = DBHelper.GetSkillModefiers();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)mods.Count);
            foreach (GR5_SkillModifier skm in mods)
                skm.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetModifiers]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
