using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GRPBackendWV
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
                    new Thread(tEventMatchStart1).Start(client);
                    break;
                case 5:
                    reply = new RMCPacketResponseAMM_Method5();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 7:
                    reply = new RMCPacketResponseAMM_Method7();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC AMMGameClient] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public static void tEventMatchStart1(object obj)
        {
            ClientInfo client = (ClientInfo)obj;
            Thread.Sleep(3000);
            RMC.SendNotification(client, 0, 1002, 2, 1, 1, 0, "amm.new.game");
            new Thread(tEventMatchStart2).Start(client);
        }

        public static void tEventMatchStart2(object obj)
        {
            ClientInfo client = (ClientInfo)obj;
            Thread.Sleep(3000);
            RMC.SendNotification(client, 0, 1002, 3, 1, 1, 0, "");
        }
    }
}
