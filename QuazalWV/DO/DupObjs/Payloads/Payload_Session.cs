using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_Session : DupObjPayload
    {
        public SharedSessionDescription sharedSesDesc = new SharedSessionDescription();
        public SessionInfo sessionInfo = new SessionInfo();
        public byte sessionState;
        public uint userDefStateM;

        public override byte[] toBuffer()
        {            
            MemoryStream m = new MemoryStream();
            m.WriteByte(1);
            sharedSesDesc.toBuffer(m);
            m.WriteByte(1);
            sessionInfo.toBuffer(m);
            m.WriteByte(1);
            Helper.WriteU8(m, sessionState);
            m.WriteByte(1);
            Helper.WriteU32(m, userDefStateM);
            return m.ToArray();
        }

        public override string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(sharedSesDesc.getDesc(tabs));
            sb.Append(sessionInfo.getDesc(tabs));
            sb.AppendLine(tabs + "[SessionState]");
            sb.AppendLine(tabs + " State = " + sessionState);
            sb.AppendLine(tabs + "[User Defined State]");
            sb.AppendLine(tabs + " State = 0x" + userDefStateM.ToString("X"));
            return sb.ToString();
        }
    }
}
