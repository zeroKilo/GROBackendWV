using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class AchievementsService
    {
        public static void HandleAchievementsServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 2:
                    reply = new RMCPacketResponseAchievementsService_GetPlayerAchievements();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 4:
                    reply = new RMCPacketResponseAchievementsService_GetPlayerAchievementGroups();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 9:
                    reply = new RMCPacketResponseAchievementsService_GetAllBaseAchievementData();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0xC:
                    //UpdatePlayerAchievementProgress, not implemented
                    reply = new RMCPResponseEmpty();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0xD:
                    reply = new RMCPacketResponseAchievementsService_GetPlayerPinnedAchievements();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC AchievementsService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
