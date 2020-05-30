using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_Session
    {
        public SharedSessionDescription sharedSesDesc = new SharedSessionDescription();
        public SessionInfo sessionInfo = new SessionInfo();
        public byte sessionState;
        public uint userDefStateM;

        public byte[] Create()
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
    }
}
