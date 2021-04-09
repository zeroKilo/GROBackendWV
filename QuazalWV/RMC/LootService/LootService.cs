using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class LootService
    {
        public static void HandleLootServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseLootService_GetLootStatic();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                //missing ds loot methods
                case 4:
                    reply = new RMCPacketResponseLootService_GetLootPointQualityMap();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 5:
                    reply = new RMCPacketResponseLootService_GetLootAssetKeyMap();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC LootService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
