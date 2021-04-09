using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseMissionService_GetPersonaMissions : RMCPResponse
    {
        public List<GR5_PersonaMission> missions = new List<GR5_PersonaMission>();
        public uint missionSeed;
        public uint missionStartTime1;//can be 64-bit value, idk
        public uint missionStartTime2;

        public RMCPacketResponseMissionService_GetPersonaMissions()
        {
            missions.Add(new GR5_PersonaMission());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)missions.Count);
            foreach (GR5_PersonaMission pm in missions)
                pm.toBuffer(m);
            Helper.WriteU32(m, missionSeed);
            Helper.WriteU32(m, missionStartTime1);
            Helper.WriteU32(m, missionStartTime2);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseMissionService_GetPersonaMissions]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
