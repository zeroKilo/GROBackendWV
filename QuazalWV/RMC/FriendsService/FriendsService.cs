using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class FriendsService
    {
        public static void ProcessFriendsServiceRequest(Stream s, RMCP rmc)
        {
            switch (rmc.methodID)
            {
                case 2:
                    rmc.request = new RMCPacketRequestFriendsService_AddFriendByName(s);
                    break;
                case 4:
                case 5:
                    break;
                default:
                    Log.WriteLine(1, "[RMC Friends] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public static void HandleFriendsServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                /*case 2:
                    RMCPacketRequestFriendsService_AddFriendByName r = (RMCPacketRequestFriendsService_AddFriendByName)rmc.request;
                    reply = new RMCPacketResponseFriendsService_AddFriendByName(client, r.name);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 4:
                    break;*/
                case 5:
                    reply = new RMCPacketResponseFriendsService_GetFriendsList(client);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC FriendsService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
