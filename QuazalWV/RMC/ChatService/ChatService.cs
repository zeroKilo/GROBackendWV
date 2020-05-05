using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class ChatService
    {
        public static void ProcessChatServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 0x5:
                    reply = new RMCPacketResponseChatService_Method5();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0x9:
                case 0xA:
                    reply = new RMCPResponseEmpty();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0xE:
                    reply = new RMCPacketResponseChatService_GetPlayerStatuses();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0x10:
                    reply = new RMCPacketResponseChatService_Method10();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC ChatService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
