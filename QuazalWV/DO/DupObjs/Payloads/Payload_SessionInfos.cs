using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_SessionInfos : DupObjPayload
    {
        public uint NbPlayers = 0;
        public SessionInfosParameter SES_SessionInfosParameter = new SessionInfosParameter();

        public override byte[] toBuffer()
        {
            MemoryStream m = new MemoryStream();
            m.WriteByte(1);
            Helper.WriteU32(m, NbPlayers);
            m.WriteByte(1); 
            SES_SessionInfosParameter.toBuffer(m);
            return m.ToArray();
        }

        public override string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tabs);
            byte[] buff = toBuffer();
            foreach (byte b in buff)
                sb.Append(b.ToString("X2") + " ");
            return sb.ToString();
        }
    }
}
