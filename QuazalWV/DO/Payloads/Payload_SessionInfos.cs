using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_SessionInfos
    {
        public byte[] Create()
        {
            MemoryStream m = new MemoryStream();
            m.WriteByte(1); // BasicUpdateProtocol<SES_SessionHeartbeat
            Helper.WriteU32(m, 0);
            m.WriteByte(1); // BasicUpdateProtocol<SES_SessionInfosParameter>
            Helper.WriteU32(m, 0);
            m.Write(new byte[256], 0, 256);
            return m.ToArray();
        }
    }
}
