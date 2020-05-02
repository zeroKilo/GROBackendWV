using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public static class AuthenticationService
    {
        public static void ProcessAuthenticationServiceRequest(Stream s, RMCP rmc)
        {
            switch (rmc.methodID)
            {
                case 2:
                    rmc.request = new RMCPacketRequestLoginCustomData(s);
                    break;
                case 3:
                    rmc.request = new RMCPacketRequestRequestTicket(s);
                    break;
                default:
                    Log.WriteLine(1, "[RMC Authentication] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }


        public static void HandleAuthenticationServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            switch (rmc.methodID)
            {
                case 2:
                    RMCPacketRequestLoginCustomData h = (RMCPacketRequestLoginCustomData)rmc.request;
                    switch (h.className)
                    {
                        case "UbiAuthenticationLoginCustomData":
                            reply = new RMCPResponseEmpty();
                            ClientInfo user = DBHelper.GetUserByName(h.username);
                            if (user != null)
                            {
                                if (user.pass == h.password)
                                {
                                    reply = new RMCPacketResponseLoginCustomData(client.PID);
                                    client.name = h.username;
                                    client.pass = h.password;
                                    client.sessionKey = ((RMCPacketResponseLoginCustomData)reply).ticket.sessionKey;
                                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                                }
                                else
                                {
                                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply, true, 0x80030065);
                                }
                            }
                            else
                            {
                                RMC.SendResponseWithACK(client.udp, p, rmc, client, reply, true, 0x80030064);
                            }
                            break;
                        default:
                            Log.WriteLine(1, "[RMC Authentication] Error: Unknown Custom Data class " + h.className);
                            break;
                    }
                    break;
                case 3:
                    reply = new RMCPacketResponseRequestTicket(client.PID);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC Authentication] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }
    }
}
