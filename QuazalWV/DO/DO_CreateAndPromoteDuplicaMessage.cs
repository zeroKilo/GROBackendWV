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
        public static uint clientStation = 0x5C00004;
        public static uint masterStation = 0x5C00001;
        public static byte unk1 = 2;
        public static uint unk2;
        public static byte[] HandlePacket(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling CreateAndPromoteDuplicaMessage...");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x13);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, clientStation);
            Helper.WriteU32(m, masterStation);
            m.WriteByte(unk1);
            Helper.WriteU32(m, unk2);
            MakeDiscoveryMessage(m);
            return m.ToArray();
        }

        private static void MakeDiscoveryMessage(MemoryStream m)
        {
            m.WriteByte(1);
            new DS_ConnectionInfo().toBuffer(m);
            m.WriteByte(1);
            new StationIdentification().toBuffer(m);
            m.WriteByte(1);
            new StationInfo().toBuffer(m);
            m.WriteByte(1);
            Helper.WriteU16(m, 1); //StationState
        }
    }
}
