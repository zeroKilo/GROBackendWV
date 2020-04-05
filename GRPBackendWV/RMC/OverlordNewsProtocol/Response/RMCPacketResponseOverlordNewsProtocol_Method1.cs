using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseOverlordNewsProtocol_Method1 : RMCPacketReply
    {
        public List<GR5_NewsMessage> news = new List<GR5_NewsMessage>();

        public RMCPacketResponseOverlordNewsProtocol_Method1(ClientInfo client)
        {
            news = DBHelper.GetNews(client.PID);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)news.Count);
            foreach (GR5_NewsMessage n in news)
                n.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseOverlordNewsProtocol_Method1]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
