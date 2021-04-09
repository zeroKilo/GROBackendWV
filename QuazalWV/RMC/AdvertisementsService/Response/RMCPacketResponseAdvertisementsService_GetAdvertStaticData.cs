using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAdvertisementsService_GetAdvertStaticData : RMCPResponse
    {
        //probably unnecessary
        public class StaticLists
        {
            public List<GR5_AdStaticList> list = new List<GR5_AdStaticList>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, (uint)list.Count);
                foreach (GR5_AdStaticList a in list)
                    a.toBuffer(s);
            }
        }

        public List<GR5_AdContainer> adContainers = new List<GR5_AdContainer>();
        public List<GR5_AdServer> adServers = new List<GR5_AdServer>();
        public List<StaticLists> staticLists = new List<StaticLists>();
        public List<GR5_AdRecommender> adRecommenders = new List<GR5_AdRecommender>();

        public RMCPacketResponseAdvertisementsService_GetAdvertStaticData()
        {
            adContainers.Add(new GR5_AdContainer());
            adServers.Add(new GR5_AdServer());
            StaticLists u = new StaticLists();
            u.list.Add(new GR5_AdStaticList());
            staticLists.Add(u);
            adRecommenders.Add(new GR5_AdRecommender());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)adContainers.Count);
            foreach (GR5_AdContainer u in adContainers)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)adServers.Count);
            foreach (GR5_AdServer u in adServers)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)staticLists.Count);
            foreach (StaticLists u in staticLists)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)adRecommenders.Count);
            foreach (GR5_AdRecommender u in adRecommenders)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAdvertisementsService_GetAdvertStaticData]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
