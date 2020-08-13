using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_GetParticipantsResponseMessage
    {
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(2, "[DO] Handling DO_GetParticipantsResponseMessage... TODO!");
            return new byte[0];
        }

        public static byte[] Create(byte[] data)
        {
            Log.WriteLine(2, "[DO] Creating DO_GetParticipantsResponseMessage");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x15);
            m.WriteByte(1);
            Helper.WriteU32(m, 1);
            m.Write(data, 1, data.Length - 1);
            return m.ToArray();
        }
    }
}
