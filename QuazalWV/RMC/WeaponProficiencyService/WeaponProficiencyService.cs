using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class WeaponProficiencyService
    {
        public static void HandleWeaponProficiencyServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseWeaponProficiencyService_GetPersonaWeaponsXP();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 3:
                    reply = new RMCPacketResponseWeaponProficiencyService_GetWeaponsXPLevelInfo();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC WeaponProficiencyService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
