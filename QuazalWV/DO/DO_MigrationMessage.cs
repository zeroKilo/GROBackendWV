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
        public static uint fromStationID = 0x5C00002;
        public static uint dupObj = 0x5C00002;
        public static uint toStationID = 0x5C00001;
        public static byte unk = 3;
        public static uint unk2;

        public static byte[] HandlePacket(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling MigrationMessage...");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x11);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, fromStationID);
            Helper.WriteU32(m, dupObj);
            Helper.WriteU32(m, toStationID);
            m.WriteByte(unk);
            Helper.WriteU32(m, unk2);
            return m.ToArray();
        }
    }
}
