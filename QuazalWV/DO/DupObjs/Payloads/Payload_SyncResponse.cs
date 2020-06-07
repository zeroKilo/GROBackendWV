using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_SyncResponse : DupObjPayload
    {
        public ulong time;

        public Payload_SyncResponse(ulong t)
        {
            time = t;
        }

        public override byte[] toBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU64(m, time);
            Helper.WriteU64(m, (ulong)Global.uptime.ElapsedMilliseconds);
            Helper.WriteU32(m, 0);
            return m.ToArray();
        }

        public override string getDesc()
        {
            StringBuilder sb = new StringBuilder();
            byte[] buff = toBuffer();
            foreach (byte b in buff)
                sb.Append(b.ToString("X2") + " ");
            return sb.ToString();
        }
    }
}
