using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_MigrationMessage
    {
        public static ushort callID = 3;
        public static uint fromStationID;
        public static uint recipientStationID;
        public static uint toStationID;
        public static byte unknown;

        public static byte[] HandlePacket(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling MigrationMessage...");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x11);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, fromStationID);
            Helper.WriteU32(m, recipientStationID);
            Helper.WriteU32(m, toStationID);
            m.WriteByte(unknown);
            return m.ToArray();
        }
    }
}
