using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseSkillsService_GetModifierLists : RMCPResponse
    {
        public List<GR5_SkillModifierList> sml = new List<GR5_SkillModifierList>();

        public RMCPacketResponseSkillsService_GetModifierLists()
        {
            sml = DBHelper.GetSkillModefierLists();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)sml.Count);
            foreach (GR5_SkillModifierList s in sml)
                s.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetModifierLists]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
