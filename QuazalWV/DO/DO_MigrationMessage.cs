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
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_MigrationMessage... TODO!");
            return new byte[0];
        }

        public static byte[] Create(ushort callID, uint fromStationID, uint dupObj, uint toStationID, byte version)
        {
            Log.WriteLine(1, "[DO] Creating DO_MigrationMessage");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x11);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, fromStationID);
            Helper.WriteU32(m, dupObj);
            Helper.WriteU32(m, toStationID);
            m.WriteByte(version);
            Helper.WriteU32(m, 0);
            return m.ToArray();
        }
    }
}
