using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseMissionService_GetAllMissionTemplate : RMCPacketReply
    {
        public List<GR5_Mission> missions = new List<GR5_Mission>();
        public List<GR5_MissionArc> missionArcs = new List<GR5_MissionArc>();
        public List<GR5_MissionSequence> missionSeqs = new List<GR5_MissionSequence>();

        public RMCPacketResponseMissionService_GetAllMissionTemplate()
        {
            missions.Add(new GR5_Mission());
            missionArcs.Add(new GR5_MissionArc());
            missionSeqs.Add(new GR5_MissionSequence());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)missions.Count);
            foreach (GR5_Mission mm in missions)
                mm.toBuffer(m);
            Helper.WriteU32(m, (uint)missionArcs.Count);
            foreach (GR5_MissionArc ma in missionArcs)
                ma.toBuffer(m);
            Helper.WriteU32(m, (uint)missionSeqs.Count);
            foreach (GR5_MissionSequence s in missionSeqs)
                s.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseMissionService_GetAllMissionTemplate]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
