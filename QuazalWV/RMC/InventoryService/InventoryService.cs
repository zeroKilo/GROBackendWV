using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class InventoryService
    {
        public static void ProcessInventoryServiceRequest(Stream s, RMCP rmc)
        {
            switch (rmc.methodID)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    break;
                case 6:
                    rmc.request = new RMCPacketRequestInventoryService_GetUserInventoryByBagType(s);
                    break;
                case 16:
                    break;
                default:
                    Log.WriteLine(1, "[RMC InventoryService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public static void HandleInventoryServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseInventoryService_GetTemplateItems();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 2:
                    reply = new RMCPacketResponseInventoryService_GetAllBoosts();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 3:
                    reply = new RMCPacketResponseInventoryService_GetAllConsumables();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 4:
                    reply = new RMCPacketResponseInventoryService_GetAllApplyItems();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 6:
                    RMCPacketRequestInventoryService_GetUserInventoryByBagType invByBagReq = (RMCPacketRequestInventoryService_GetUserInventoryByBagType)rmc.request;
                    reply = new RMCPacketResponseInventoryService_GetUserInventoryByBagType(invByBagReq.pid, invByBagReq.requestedBagTypes);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 16:
                    reply = new RMCPacketResponseInventoryService_GetAllDefaultLoadoutKits(client);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC InventoryService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
