using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_RMCResponseMessage
    {
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(2, "[DO] Handling DO_RMCResponseMessage... TODO!");
            return new byte[0];
        }

        public static byte[] Create(ushort callID, uint outcome, byte[] payload)
        {
            Log.WriteLine(2, "[DO] Creating DO_RMCResponseMessage");
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, 0xB);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, outcome);
            m.Write(payload, 0, payload.Length);
            return m.ToArray();
        }
    }
}
