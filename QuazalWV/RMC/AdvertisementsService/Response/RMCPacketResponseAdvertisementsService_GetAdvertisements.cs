using System.IO;
using System.Collections.Generic;
using QuazalWV.DB;

namespace QuazalWV
{
    public class RMCPacketResponseAdvertisementsService_GetAdvertisements : RMCPResponse
    {
        public List<GR5_Advertisement> Ads { get; set; }

        public RMCPacketResponseAdvertisementsService_GetAdvertisements()
        {
            Ads = AdModel.GetAds();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)Ads.Count);
            foreach (GR5_Advertisement a in Ads)
                a.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[GetAdvertisements Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
