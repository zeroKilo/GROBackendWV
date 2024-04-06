using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class ChatService
    {
        public static void ProcessChatServiceRequest(Stream s, RMCP rmc)
        {
            switch (rmc.methodID)
            {
                case 5:
                    rmc.request = new RMCPacketRequestChatService_JoinPublicChannel(s);
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 12:
                    rmc.request = new RMCPacketRequestChatService_IgnorePlayer(s);
                    break;
                case 13:
                    rmc.request = new RMCPacketRequestChatService_UnignorePlayer(s);
                    break;
                case 14:
                    break;
                case 16:
                    break;
                default:
                    Log.WriteLine(1, "[RMC Chat] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public static void HandleChatServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 5:
                    var joinPublicReq = (RMCPacketRequestChatService_JoinPublicChannel)rmc.request;
                    reply = new RMCPacketResponseChatService_JoinPublicChannel(joinPublicReq.RoomLanguage, joinPublicReq.RoomNumber);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 9:
                //SetCurrentCharacter
                case 10:
                //SetStatus
                    reply = new RMCPResponseEmpty();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 12:
                    reply = new RMCPacketResponseChatService_IgnorePlayer(((RMCPacketRequestChatService_IgnorePlayer)rmc.request).Name);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 13:
                    reply = new RMCPacketResponseChatService_UnignorePlayer(((RMCPacketRequestChatService_UnignorePlayer)rmc.request).Name);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 14:
                    reply = new RMCPacketResponseChatService_GetIgnoreList();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 16:
                    reply = new RMCPacketResponseChatService_GetMutedChannel();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC ChatService] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
