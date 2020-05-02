using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAdvertisementsService_Method1 : RMCPResponse
    {
        public List<GR5_Advertisement> ads = new List<GR5_Advertisement>();

        public RMCPacketResponseAdvertisementsService_Method1()
        {
            ads.Add(new GR5_Advertisement());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)ads.Count);
            foreach (GR5_Advertisement a in ads)
                a.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAdvertisementsService_Method1]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
