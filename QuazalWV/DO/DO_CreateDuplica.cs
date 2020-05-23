using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_CreateDuplica
    {        
        public static byte[] HandlePacket(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_CreateDuplica... TODO!");
            return new byte[0];
        }

        public static byte[] Create(ClientInfo client, uint DupObj, uint Station, byte version, byte[] payload)
        {
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x12);
            Helper.WriteU32(m, DupObj);
            Helper.WriteU32(m, Station);
            Helper.WriteU8(m, version);
            m.Write(payload, 0, payload.Length);
            return m.ToArray();
        }
    }
}
