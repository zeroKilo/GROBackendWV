using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public enum STATIONSTATE
    {
        Unknown = 0,
        JoiningSession = 1,
        CreatingSession = 2,
        Participating = 3,
        PreparingToLeave = 4,
        Leaving = 5,
        LeavingOnFault = 6
    }

    public class Payload_Station : DupObjPayload
    {
        public DS_ConnectionInfo connectionInfo = new DS_ConnectionInfo();
        public StationIdentification stationIdent = new StationIdentification();
        public StationInfo stationInfo = new StationInfo();
        public STATIONSTATE stationState;

        public Payload_Station() { }
        public Payload_Station(Stream s)
        {
            if (Helper.ReadU8(s) == 1)
                connectionInfo = new DS_ConnectionInfo(s);
            if (Helper.ReadU8(s) == 1)
                stationIdent = new StationIdentification(s);
            if (Helper.ReadU8(s) == 1)
                stationInfo = new StationInfo(s);
            if (Helper.ReadU8(s) == 1)
                stationState = (STATIONSTATE)Helper.ReadU16(s);
        }

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
            Helper.WriteU16(m, (ushort)stationState);
            return m.ToArray();
        }

        public override string getDesc()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(connectionInfo.getDesc());
            sb.Append(stationIdent.getDesc());
            sb.Append(stationInfo.getDesc());
            sb.AppendLine("[StationState]");
            sb.AppendLine(" State = " + stationState);
            return sb.ToString();
        }
    }
}
