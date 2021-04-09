using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseSkillsService_GetCharacterSkillsByID : RMCPResponse
    {
        //not tested yet
        List<GR5_Skill> skills = new List<GR5_Skill>();

        public RMCPacketResponseSkillsService_GetCharacterSkillsByID()
        {
            skills.Add(new GR5_Skill());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            foreach (GR5_Skill s in skills) s.toBuffer(m);
            //Helper.WriteU32(m, 0);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetCharacterSkillsByID]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
