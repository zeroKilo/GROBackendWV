using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAdvertisementsService_Method2 : RMCPacketReply
    {
        public class Unknown
        {
            public List<GR5_AdStaticList> list = new List<GR5_AdStaticList>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, (uint)list.Count);
                foreach (GR5_AdStaticList a in list)
                    a.toBuffer(s);
            }
        }

        public List<GR5_AdContainer> unk1 = new List<GR5_AdContainer>();
        public List<GR5_AdServer> unk2 = new List<GR5_AdServer>();
        public List<Unknown> unk3 = new List<Unknown>();
        public List<GR5_AdRecommender> unk4 = new List<GR5_AdRecommender>();

        public RMCPacketResponseAdvertisementsService_Method2()
        {
            unk1.Add(new GR5_AdContainer());
            unk2.Add(new GR5_AdServer());
            Unknown u = new Unknown();
            u.list.Add(new GR5_AdStaticList());
            unk3.Add(u);
            unk4.Add(new GR5_AdRecommender());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (GR5_AdContainer u in unk1)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)unk2.Count);
            foreach (GR5_AdServer u in unk2)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)unk3.Count);
            foreach (Unknown u in unk3)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)unk4.Count);
            foreach (GR5_AdRecommender u in unk4)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAdvertisementsService_Method2]";
        }
    }
}
