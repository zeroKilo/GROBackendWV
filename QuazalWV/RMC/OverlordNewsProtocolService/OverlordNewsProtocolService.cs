using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class OverlordNewsProtocolService
    {
        public enum REQUEST
        {
            SystemNews = 0,
            PersonaNews = 1,
            FriendsNews = 2
        }

        public static int lastRequest = -1;

        public static void HandleOverlordNewsProtocolRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            lastRequest++;
            switch (rmc.methodID)
            {
                case 1:
                    switch((REQUEST)(lastRequest % 3))
                    {
                        case REQUEST.SystemNews:
                            reply = new RMCPacketResponseOverlordNewsProtocol_GetNews(client, REQUEST.SystemNews);
                            RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                            break;
                        case REQUEST.PersonaNews:
                            reply = new RMCPacketResponseOverlordNewsProtocol_GetNews(client, REQUEST.PersonaNews);
                            RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                            break;
                        case REQUEST.FriendsNews:
                            reply = new RMCPacketResponseOverlordNewsProtocol_GetNews(client, REQUEST.FriendsNews);
                            RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                            break;
                    }
                    break;
                default:
                    Log.WriteLine(1, "[RMC OverlordNewsProtocolService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public static void ProcessOverlordNewsServiceRequest(Stream s, RMCP rmc)
        {
            string predicateName = Helper.ReadString(s);
            switch (rmc.methodID)
            {
                case 1:
                    switch (predicateName)
                    {
                        case "ProtoPredicateGameNews":
                            rmc.request = new RMCPacketGetSystemNewsRequest(s);
                            break;
                        case "ProtoPredicatePrincipals":
                            rmc.request = new RMCPacketGetPlayerNewsRequest(s);
                            break;
                        default:
                            Log.WriteLine(1, "[RMC OverlordNewsProtocolService] Error: Unknown predicate type " + predicateName);
                            break;
                    }
                    break;
            }
        }
    }
}
