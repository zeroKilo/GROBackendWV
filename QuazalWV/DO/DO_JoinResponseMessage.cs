using System;
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
            return new byte[] { 0x01, 0x01, 0x04, 0x00, 0xC0, 0x05, 0x78, 0x56, 0x34, 0x12, 0x00, 0x00 };
        }
    }
}
