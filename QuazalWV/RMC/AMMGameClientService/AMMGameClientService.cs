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
                    uint matchReqId = 1;
                    uint sesId = 1;
                    var joinType = "amm.new.game";
                    // RDV_E_AMM_EVENT_SESSION_FOUND
                    NotificationQuene.AddNotification(new NotificationQueneEntry(client, 3000, 0, 1002, 2, matchReqId, sesId, 0, joinType));
                    sesId = 1;
                    uint teamId = 1;
                    var gameMode = "1";
                    // CallGetSessionURLs
                    NotificationQuene.AddNotification(new NotificationQueneEntry(client, 6000, 0, 1002, 3, sesId, teamId, 0, gameMode));
                    break;
                case 5:
                    reply = new RMCPacketResponseAMMGameClientService_LeaveAMMSearch();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 7:
                    reply = new RMCPacketResponseAMMGameClientService_GetActiveAMMPlaylists();
                    // RDV_E_AMM_NOTIFICATION_SESSIONTAPPED
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
