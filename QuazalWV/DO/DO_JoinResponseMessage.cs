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
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_JoinResponseMessage... TODO!");
            return new byte[0];
        }

        public static byte[] Create(byte successByte, uint clientStationID, uint endPointConnectionID)
        {
            Log.WriteLine(1, "[DO] Creating DO_JoinResponseMessage");
            MemoryStream m = new MemoryStream();
            m.WriteByte(1);
            m.WriteByte(successByte);
            if (successByte == 1)
            {
                Helper.WriteU32(m, clientStationID);
                Helper.WriteU32(m, endPointConnectionID);
                Helper.WriteU16(m, 0);
            }
            else
                Log.WriteLine(1, "[DO] Creating negative DO_JoinResponseMessage failed, TODO!");
            return m.ToArray();
        }
    }
}
