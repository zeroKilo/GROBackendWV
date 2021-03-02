using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QuazalWV
{
    public static class AMMGameClientService
    {
        public static void HandleAMMGameClientRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 2:
                    reply = new RMCPacketResponseAMM_GetSessionURLs();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 4:
                    reply = new RMCPacketResponseAMM_RequestAMMSearch();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    NotificationQuene.AddNotification(new NotificationQueneEntry(client, 3000, 0, 1002, 2, 1, 1, 0, "amm.new.game"));
                    NotificationQuene.AddNotification(new NotificationQueneEntry(client, 6000, 0, 1002, 3, 1, 1, 0, "1"));
                    break;
                case 5:
                    reply = new RMCPacketResponseAMM_LeaveAMMSearch();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 7:
                    reply = new RMCPacketResponseAMM_FetchAMMPlaylists();
                    NotificationQuene.AddNotification(new NotificationQueneEntry(client, 3000, 0, 1002, 1, 1, 1, 0, "7"));
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC AMMGameClient] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
