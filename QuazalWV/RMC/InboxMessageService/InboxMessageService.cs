using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class InboxMessageService
    {
        public static void ProcessInboxMessageServiceRequest(Stream s, RMCP rmc)
        {
            switch (rmc.methodID)
            {
                case 1:
                    break;
                case 2:
                    rmc.request = new RMCPacketRequestInboxMessageService_GetRecentInboxMessages(s);
                    break;
                case 7:
                    rmc.request = new RMCPacketRequestInboxMessageService_SetReadFlags(s);
                    break;
                default:
                    Log.WriteLine(1, "[RMC InboxMessageService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public static void HandleInboxMessageServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseInboxMessageService_GetInboxMessageOasisIdDict();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 2:
                    reply = new RMCPacketResponseInboxMessageService_GetRecentInboxMessages();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 7:
                    reply = new RMCPResponseEmpty();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC InboxMessageService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
