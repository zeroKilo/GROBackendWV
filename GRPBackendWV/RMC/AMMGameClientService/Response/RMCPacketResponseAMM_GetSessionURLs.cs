using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    class RMCPacketResponseAMM_GetSessionURLs : RMCPResponse
    {
        List<string> urls = new List<string>();
        public uint sessionID;

        public RMCPacketResponseAMM_GetSessionURLs()
        {
            string url = "prudps:/address=127.0.0.1;port=21030;CID=1;PID=4096;sid=1;stream=3;type=2";
            urls.Add(url);
            sessionID = 1;
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteStringList(m, urls);
            Helper.WriteU32(m, sessionID);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAMM_GetSessionURLs]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}