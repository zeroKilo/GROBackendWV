using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_GetParticipantsRequest
    {
        public static void HandlePacket(ClientInfo client, QPacket p)
        {
            Log.WriteLine(1, "[DO] Handling GetParticipantsRequest...");
        }
    }
}
