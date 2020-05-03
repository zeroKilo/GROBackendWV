using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public static class AMMDedicatedServerService
    {
        public static void HandleAMMDedicatedServerServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 6:
                    reply = new RMCPacktResponseAMMDS_Method6();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC AMMDedicatedServerService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
