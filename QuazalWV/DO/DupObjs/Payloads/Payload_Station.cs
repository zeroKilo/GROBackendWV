using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_Station : DupObjPayload
    {
        public DS_ConnectionInfo connectionInfo = new DS_ConnectionInfo();
        public StationIdentification stationIdent = new StationIdentification();
        public StationInfo stationInfo = new StationInfo();
        public ushort stationState = 3;

        public override byte[] toBuffer()
        {
            MemoryStream m = new MemoryStream();
            m.WriteByte(1);
            connectionInfo.toBuffer(m);
            m.WriteByte(1);
            stationIdent.toBuffer(m);
            m.WriteByte(1);
            stationInfo.toBuffer(m);
            m.WriteByte(1);
            Helper.WriteU16(m, stationState);
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
