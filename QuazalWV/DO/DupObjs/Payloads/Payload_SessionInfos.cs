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
        public byte[] sessionParams = new byte[256];
        public List<SessionInfosParameter> SES_SessionInfosParameter = new List<SessionInfosParameter>();

        public override byte[] toBuffer()
        {
            SES_SessionInfosParameter = new List<SessionInfosParameter>();
            SessionInfosParameter info = new SessionInfosParameter();
            info.m_bSessionParametersAreSet = true;
            for (int i = 0; i < 256; i++)
            {
                info.m_cSessionParameters[i] = (byte)i;
                sessionParams[i] = (byte)i;
            }
            SES_SessionInfosParameter.Add(info);

            MemoryStream m = new MemoryStream();
            m.WriteByte(1); // BasicUpdateProtocol<SES_SessionHeartbeat>
            Helper.WriteU32(m, 0);
            m.WriteByte(1); 
            Helper.WriteU32(m, (uint)SES_SessionInfosParameter.Count);
            foreach (SessionInfosParameter p in SES_SessionInfosParameter)
                p.toBuffer(m);
            m.Write(sessionParams, 0, 256);
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
