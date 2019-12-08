using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseMissionService_Method3 : RMCPacketReply
    {
        public List<GR5_PersonaMission> list = new List<GR5_PersonaMission>();
        public uint unk1;
        public uint unk2;
        public uint unk3;

        public RMCPacketResponseMissionService_Method3()
        {
            list.Add(new GR5_PersonaMission());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_PersonaMission pm in list)
                pm.toBuffer(m);
            Helper.WriteU32(m, unk1);
            Helper.WriteU32(m, unk2);
            Helper.WriteU32(m, unk3);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseMissionService_Method3]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
