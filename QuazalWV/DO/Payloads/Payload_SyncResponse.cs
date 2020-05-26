using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_SyncResponse
    {
        public static byte[] Create(ulong time)
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU64(m, time);
            Helper.WriteU64(m, (ulong)Global.uptime.ElapsedMilliseconds);
            Helper.WriteU32(m, 0);
            return m.ToArray();
        }
    }
}
