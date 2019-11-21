using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAchievementsService_MethodD : RMCPacketReply
    {
        List<uint> unk1 = new List<uint>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (uint u in unk1)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAchievementsService_MethodD]";
        }
    }
}
