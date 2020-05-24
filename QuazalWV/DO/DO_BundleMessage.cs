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
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_BundleMessage... TODO!");
            return new byte[0];
        }

        public static byte[] Create(ClientInfo client, List<byte[]> data)
        {
            Log.WriteLine(1, "[DO] Creating DO_BundleMessage");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0xF);
            foreach (byte[] buff in data)
            {
                Helper.WriteU32(m, (uint)buff.Length);
                m.Write(buff, 0, buff.Length);
            }
            Helper.WriteU32(m, 0);
            return m.ToArray();
        }
    }
}
