using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class SkillService
    {
        public static void HandleSkillsServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseSkillsService_GetGameClass();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 2:
                    reply = new RMCPacketResponseSkillsService_GetSkills();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 3:
                    reply = new RMCPacketResponseSkillsService_GetSkillPowers();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 4:
                    reply = new RMCPacketResponseSkillsService_GetModifierLists();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 5:
                    reply = new RMCPacketResponseSkillsService_GetModifiers();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 7:
                    reply = new RMCPacketResponseSkillsService_GetCharacterSkillsByID();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC SkillService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
