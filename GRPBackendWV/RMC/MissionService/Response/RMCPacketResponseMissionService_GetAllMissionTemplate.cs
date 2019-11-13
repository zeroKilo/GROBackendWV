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
        public class Mission
        {
            public uint unk1;
            public string unk2;
            public uint[] unk3 = new uint[4];
            public byte[] unk4 = new byte[7];
            public uint unk5;

            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteString(s, unk2);
                foreach (uint u in unk3)
                    Helper.WriteU32(s, u);
                s.Write(unk4, 0, 7);
                Helper.WriteU32(s, unk5);
            }
        }

        public class MissionArc
        {
            public uint[] unk1 = new uint[4];
            public byte[] unk2 = new byte[4];
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
                s.Write(unk2, 0, 4);
            }
        }

        public class MissionSequence
        {
            public uint[] unk1 = new uint[4];
            public byte unk2;
            public uint[] unk3 = new uint[4];

            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
                Helper.WriteU8(s, unk2);
                foreach (uint u in unk3)
                    Helper.WriteU32(s, u);
            }
        }

        public List<Mission> missions = new List<Mission>();
        public List<MissionArc> missionArcs = new List<MissionArc>();
        public List<MissionSequence> missionSeqs = new List<MissionSequence>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)missions.Count);
            foreach (Mission mm in missions)
                mm.toBuffer(m);
            Helper.WriteU32(m, (uint)missionArcs.Count);
            foreach (MissionArc ma in missionArcs)
                ma.toBuffer(m);
            Helper.WriteU32(m, (uint)missionSeqs.Count);
            foreach (MissionSequence s in missionSeqs)
                s.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseMissionService_GetAllMissionTemplate]";
        }
    }
}
