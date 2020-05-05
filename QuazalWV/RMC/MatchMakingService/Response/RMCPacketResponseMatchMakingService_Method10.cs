using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseMatchMakingService_Method10 : RMCPResponse
    {

        public List<string> stationURLs = new List<string>();

        public RMCPacketResponseMatchMakingService_Method10()
        {
            stationURLs.Add(Global.sessionURL);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteStringList(m, stationURLs);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseMatchMakingService_Method10]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
