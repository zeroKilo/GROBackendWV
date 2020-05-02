using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSkillsService_Method3 : RMCPResponse
    {
        public List<GR5_SkillPower> list = new List<GR5_SkillPower>();

        public RMCPacketResponseSkillsService_Method3()
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
            return "[RMCPacketResponseSkillsService_Method3]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
