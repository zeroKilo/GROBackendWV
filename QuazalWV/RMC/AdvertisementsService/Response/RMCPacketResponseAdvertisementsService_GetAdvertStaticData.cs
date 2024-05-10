using System.IO;
using System.Collections.Generic;
using QuazalWV.DB;

namespace QuazalWV
{
    public class RMCPacketResponseAdvertisementsService_GetAdvertStaticData : RMCPResponse
    {
        public class StaticLists
        {
            public List<GR5_AdStaticList> Lists { get; set; }

            public StaticLists()
            {
                Lists = AdModel.GetAdStaticLists();
            }

            public void ToBuffer(Stream s)
            {
                Helper.WriteU32(s, (uint)Lists.Count);
                foreach (GR5_AdStaticList a in Lists)
                    a.ToBuffer(s);
            }
        }

        public List<GR5_AdContainer> AdContainers { get; set; }
        public List<GR5_AdServer> AdServers { get; set; }
        public List<StaticLists> StaticAdLists { get; set; }
        public List<GR5_AdRecommender> AdRecommenders { get; set; }

        public RMCPacketResponseAdvertisementsService_GetAdvertStaticData()
        {
            
            AdContainers = AdModel.GetAdContainers();
            AdServers = AdModel.GetAdServers();
            StaticAdLists = new List<StaticLists> { new StaticLists() };
            AdRecommenders = AdModel.GetAdRecommenders();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)AdContainers.Count);
            foreach (GR5_AdContainer u in AdContainers)
                u.ToBuffer(m);
            Helper.WriteU32(m, (uint)AdServers.Count);
            foreach (GR5_AdServer u in AdServers)
                u.ToBuffer(m);
            Helper.WriteU32(m, (uint)StaticAdLists.Count);
            foreach (StaticLists u in StaticAdLists)
                u.ToBuffer(m);
            Helper.WriteU32(m, (uint)AdRecommenders.Count);
            foreach (GR5_AdRecommender u in AdRecommenders)
                u.ToBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[GetAdvertStaticData Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
