using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class SecureService
    {
        public static void ProcessSecureServiceRequest(Stream s, RMCP rmc)
        {
            switch (rmc.methodID)
            {
                case 4:
                    rmc.request = new RMCPacketRequestRegisterEx(s);
                    break;
                default:
                    Log.WriteLine(1, "[RMC Secure] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }


        public static void HandleSecureServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 4:
                    RMCPacketRequestRegisterEx h = (RMCPacketRequestRegisterEx)rmc.request;
                    switch (h.className)
                    {
                        case "UbiAuthenticationLoginCustomData":
                            reply = new RMCPacketResponseRegisterEx(client.PID);
                            RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                            break;
                        default:
                            Log.WriteLine(1, "[RMC Secure] Error: Unknown Custom Data class " + h.className);
                            break;
                    }
                    break;
                default:
                    Log.WriteLine(1, "[RMC Secure] Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
