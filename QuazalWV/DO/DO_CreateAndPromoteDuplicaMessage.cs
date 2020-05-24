using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_CreateAndPromoteDuplicaMessage
    {
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_CreateAndPromoteDuplicaMessage... TODO!");
            return new byte[0];
        }

        public static byte[] Create(ushort callID, uint dupObj, uint masterStation, byte version, byte[] payload)
        {
            Log.WriteLine(1, "[DO] Creating DO_CreateAndPromoteDuplicaMessage");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x13);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, dupObj);
            Helper.WriteU32(m, masterStation);
            m.WriteByte(version);
            Helper.WriteU32(m, 0);
            m.Write(payload, 0, payload.Length);
            return m.ToArray();
        }
    }
}
