using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class PlayerProfileService
    {
        public static void HandlePlayerProfileServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 2:
                    reply = new RMCPResponseEmpty();
                    break;
                case 0xF:
                    reply = new RMCPacketResponsePlayerProfileService_GetAllFaceSkinTones();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0x10:
                    reply = new RMCPResponseEmpty();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0x11:
                    reply = new RMCPacketResponsePlayerProfileService_Method11();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0x12:
                    reply = new RMCPacketResponsePlayerProfileService_LoadCharacterProfiles(client);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC PlayerProfileService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
