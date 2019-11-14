using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAchievementsService_Method9 : RMCPacketReply
    {
        public class Achievement
        {
            public uint[] unk1 = new uint[7];
            public string unk2;
            public uint[] unk3 = new uint[4];
            public byte[] unk4 = new byte[6];
            public uint unk5;
            public List<uint> unk6 = new List<uint>();
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
                Helper.WriteString(s, unk2);
                foreach (uint u in unk3)
                    Helper.WriteU32(s, u);
                s.Write(unk4, 0, 6);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, (uint)unk6.Count());
                foreach (uint u in unk6)
                    Helper.WriteU32(s, u);
            }
        }

        public class AchievementGroup
        {
            public uint[] unk1 = new uint[9];
            public List<uint> unk2 = new List<uint>();
            public List<uint> unk3 = new List<uint>();
            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
                Helper.WriteU32(s, (uint)unk2.Count());
                foreach (uint u in unk2)
                    Helper.WriteU32(s, u);
                Helper.WriteU32(s, (uint)unk3.Count());
                foreach (uint u in unk3)
                    Helper.WriteU32(s, u);
            }
        }

        public class AchievementCategory
        {
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
            }
        }

        public List<Achievement> achs = new List<Achievement>();
        public List<AchievementGroup> groups = new List<AchievementGroup>();
        public List<AchievementCategory> cats = new List<AchievementCategory>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)achs.Count());
            foreach (Achievement a in achs)
                a.toBuffer(m);
            Helper.WriteU32(m, (uint)groups.Count());
            foreach (AchievementGroup g in groups)
                g.toBuffer(m);
            Helper.WriteU32(m, (uint)cats.Count());
            foreach (AchievementCategory c in cats)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAchievementsService_Method9]";
        }
    }
}
