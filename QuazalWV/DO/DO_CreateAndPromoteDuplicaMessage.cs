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
        public static ushort callID = 3;
        public static uint DupObj = 0x5C00002;
        public static uint masterStation = 0x5C00001;
        public static byte unk1 = 2;
        public static uint unk2;
        public static byte[] HandlePacket(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling CreateAndPromoteDuplicaMessage...");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x13);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, DupObj);
            Helper.WriteU32(m, masterStation);
            m.WriteByte(unk1);
            Helper.WriteU32(m, unk2);
            byte[] payload = DupCreateMasterStation.CreatePayload();
            m.Write(payload, 0, payload.Length);
            return m.ToArray();
        }
    }
}
