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
        public static byte successByte = 1;
        public static uint clientStationID = 0x5c00004;
        public static uint endPointConnectionID = 0x5c00001;
        public static byte[] HandlePacket(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling JoinRequest...");
            MemoryStream m = new MemoryStream();
            m.WriteByte(1);
            m.WriteByte(successByte);
            Helper.WriteU32(m, clientStationID);
            Helper.WriteU32(m, endPointConnectionID);
            Helper.WriteU16(m, 0);
            return m.ToArray();
        }
    }
}
