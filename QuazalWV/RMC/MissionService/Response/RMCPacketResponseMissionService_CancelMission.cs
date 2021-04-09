using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseMissionService_CancelMission : RMCPResponse
    {
        uint unused;
        uint missionId;

        public RMCPacketResponseMissionService_CancelMission()
        {
            unused = 0x11;
            missionId = 0;
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, unused);
            Helper.WriteU32(m, missionId);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseMissionService_CancelMission]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
