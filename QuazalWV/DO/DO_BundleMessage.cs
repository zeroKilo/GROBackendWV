using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_BundleMessage
    {
        public static byte[] HandlePacket(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling BundleMessage... TODO!");
            return new byte[0];
        }

        public static byte[] Create(ClientInfo client, List<byte[]> data, uint unk1, uint unk2)
        {
            MemoryStream m = new MemoryStream();
            m.WriteByte(0xF);
            foreach (byte[] buff in data)
            {
                Helper.WriteU32(m, (uint)buff.Length);
                m.Write(buff, 0, buff.Length);
            }
            Helper.WriteU32(m, unk1);
            Helper.WriteU32(m, unk2);
            return m.ToArray();
        }
    }
}
