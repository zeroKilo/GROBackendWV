using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSkillsService_GetGameClass : RMCPacketReply
    {
        public List<GR5_GameClass> _GameClassVector = new List<GR5_GameClass>();

        public RMCPacketResponseSkillsService_GetGameClass()
        {
            _GameClassVector.Add(new GR5_GameClass());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)_GameClassVector.Count);
            foreach (GR5_GameClass g in _GameClassVector)
                g.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetGameClass]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
