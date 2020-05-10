using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_JoinResponseMessage
    {
        public static byte[] HandlePacket(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling JoinRequest...");
            MemoryStream m = new MemoryStream();
            m.WriteByte(1);
            m.WriteByte(1);
            Helper.WriteU32(m, 0x5c00004);
            Helper.WriteU32(m, 0x5c00001);
            Helper.WriteU16(m, 0);
            return m.ToArray();
        }
    }
}
