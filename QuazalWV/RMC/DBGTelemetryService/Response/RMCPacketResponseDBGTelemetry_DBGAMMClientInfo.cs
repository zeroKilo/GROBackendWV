using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuazalWV
{
    /// <summary>
    /// Network info for the current playlist (the current playlist is set via AMM notifications 1 and 3), sent every 10 seconds
    /// </summary>
    public class RMCPacketResponseDBGTelemetry_DBGAMMClientInfo : RMCPResponse
    {
        // xml data processed exactly like news messages
        //example
        public string xmlBody;

        public RMCPacketResponseDBGTelemetry_DBGAMMClientInfo()
        {
            xmlBody = GetCurrInfo();
        }

        /// <summary>
        /// Gets XML data for the client to estimate network activity. TODO: needs a switch for different playlist infos, for now they all have this one
        /// </summary>
        /// <returns></returns>
        public string GetCurrInfo()
        {
            return
                //cdbg = client debug
                new XElement("cdbg",
                // ammi = AMM Info
                new XElement("ammi",
                    //actp = active players
                    new XAttribute("actp", "8000000000"),
                    //expq = expired queue (server timeout value)
                    new XAttribute("expq", "3000"),
                    //scnt = session count
                    new XAttribute("scnt", "8000000000"),
                    //mapn = map index (not assigned)
                    new XAttribute("mapn", "8000000000"),
                    //idle = idle server count
                    new XAttribute("idle", "8000000000"),
                    //pque = packets in queue (nb of requests)
                    new XAttribute("pque", "8000000000"),
                    //hjcn = hot join count (approximate number of hot join slots)
                    new XAttribute("hjcn", "8000000000")
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }
        

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteString(m, xmlBody);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseDBGTelemetry_DBGAMMClientInfo]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
