using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_GetParticipantsRequestMessage
    {
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_GetParticipantsRequestMessage...");
            return DO_GetParticipantsResponseMessage.Create();
        }
    }
}
