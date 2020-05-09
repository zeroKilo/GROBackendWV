using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_GetParticipantsRequest
    {
        public static byte[] HandlePacket(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling GetParticipantsRequest...");
            return new byte[] { 0x15, 0x01, 0x00, 0x00, 0x00, 0x00 };
        }
    }
}
