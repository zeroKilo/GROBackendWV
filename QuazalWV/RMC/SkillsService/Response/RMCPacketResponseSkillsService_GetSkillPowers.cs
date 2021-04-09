using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseSkillsService_GetSkillPowers : RMCPResponse
    {
        public List<GR5_SkillPower> list = new List<GR5_SkillPower>();

        public RMCPacketResponseSkillsService_GetSkillPowers()
        {
            list.Add(new GR5_SkillPower());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_SkillPower sp in list)
                sp.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetSkillPowers]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
