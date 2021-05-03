using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class PlayerProfileService
    {
        public static void ProcessPlayerProfileServiceRequest(Stream s, RMCP rmc)
        {
            switch (rmc.methodID)
            {
                case 2:
                    break;
                case 7:
                    rmc.request = new RMCPacketRequestPlayerProfileService_SetAvatarPortrait(s);
                    break;
                case 8:
                    rmc.request = new RMCPacketRequestPlayerProfileService_SetAvatarDecorator(s);
                    break;
                case 0xF:
                case 0x10:
                case 0x11:
                case 0x12:
                    break;
                default:
                    Log.WriteLine(1, "[RMC PlayerProfile] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public static void HandlePlayerProfileServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 2:
                    //ChangePersonaName, not implemented
                    reply = new RMCPResponseEmpty();
                    break;
                case 7:
                    RMCPacketRequestPlayerProfileService_SetAvatarPortrait setPortReq = (RMCPacketRequestPlayerProfileService_SetAvatarPortrait)rmc.request;
                    DBHelper.SetAvatarPortrait(client, setPortReq.portraitId, setPortReq.backgroundColor);
                    reply = new RMCPacketResponsePlayerProfileService_SetAvatarPortrait(client, setPortReq.portraitId, setPortReq.backgroundColor);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 8:
                    RMCPacketRequestPlayerProfileService_SetAvatarDecorator setDecoReq = (RMCPacketRequestPlayerProfileService_SetAvatarDecorator)rmc.request;
                    DBHelper.SetAvatarDecorator(client, setDecoReq.decoratorId);
                    reply = new RMCPacketResponsePlayerProfileService_SetAvatarDecorator(client, setDecoReq.decoratorId);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0xF:
                    reply = new RMCPacketResponsePlayerProfileService_GetAllFaceSkinTones();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0x10:
                    //SetPlayerFaceSkinTone, not implemented
                    reply = new RMCPResponseEmpty();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0x11:
                    reply = new RMCPacketResponsePlayerProfileService_RetrieveOfflineNotifications();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 0x12:
                    reply = new RMCPacketResponsePlayerProfileService_GetProfileData(client);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC PlayerProfileService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
