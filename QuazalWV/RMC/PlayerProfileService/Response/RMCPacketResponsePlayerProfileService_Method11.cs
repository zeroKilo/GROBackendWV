using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponsePlayerProfileService_RetrieveOfflineNotifications : RMCPResponse
    {
        public List<GR5_Notification> list = new List<GR5_Notification>();

        public RMCPacketResponsePlayerProfileService_RetrieveOfflineNotifications()
        {
            list.Add(new GR5_Notification());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_Notification n in list)
                n.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_RetrieveOfflineNotifications]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
