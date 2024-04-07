using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class PartyService
    {
        public static void ProcessPartyServiceRequest(Stream s, RMCP rmc)
        {
            switch (rmc.methodID)
            {
                case 1:
                    break;
                case 2:
                    rmc.request = new RMCPacketRequestPartyService_InviteByID(s);
                    break;
                case 4:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                default:
                    Log.WriteLine(1, "[RMC Party] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public static void HandlePartyServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponsePartyService_OnSignInCheckPartyStatus();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 2:
                    var inviteIdReq = (RMCPacketRequestPartyService_InviteByID)rmc.request;
                    reply = new RMCPacketResponsePartyService_InviteByID(inviteIdReq.Pids[0]);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 4:
                    reply = new RMCPacketResponsePartyService_GetInviteList();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 7:
                //DeclinePartyInvite
                case 8:
                //CancelPartyInvite
                case 9:
                //PromoteToLeader
                case 11:
                //RemoveFromParty
                case 12:
                //LeaveParty
                case 13:
                //DisbandParty
                    reply = new RMCPResponseEmpty();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC PartyService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
