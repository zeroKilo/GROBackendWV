using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_CreateDuplicaMessage
    {        
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_CreateDuplicaMessage... TODO!");
            return new byte[0];
        }

        public static byte[] Create(ClientInfo client, DupObj obj, byte version)
        {
            Log.WriteLine(1, "[DO] Creating DO_CreateDuplicaMessage");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x12);
            Helper.WriteU32(m, obj);
            Helper.WriteU32(m, obj.Master);
            Helper.WriteU8(m, version);
            byte[] payload = obj.getPayload();
            m.Write(payload, 0, payload.Length);
            return m.ToArray();
        }
    }
}
