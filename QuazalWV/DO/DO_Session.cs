using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_Session
    {
        public static uint ID = 1;
        public static uint MatchID = 1;

        public static List<DupObj> DupObjs = new List<DupObj>();

        public static void ResetObjects()
        {
            DupObjs = new List<DupObj>();
            Payload_Station ps = new Payload_Station();
            ps.connectionInfo.m_strStationURL1 = "prudp:/address=127.0.0.1;port=5004;RVCID=166202";
            ps.connectionInfo.m_strStationURL2 = "prudp:/address=127.0.0.1;port=5004;sid=15;type=2;RVCID=166202";
            ps.stationState = STATIONSTATE.Participating;
            DupObjs.Add(new DupObj(DupObjClass.Station, 1, 1, ps));
            DupObjs.Add(new DupObj(DupObjClass.SessionClock, 1, 1));
            DupObjs.Add(new DupObj(DupObjClass.SES_cl_SessionInfos, 2, 1, new Payload_SessionInfos()));
            DupObjs.Add(new DupObj(DupObjClass.PromotionReferee, 3, 1));
            DupObjs.Add(new DupObj(DupObjClass.Session, 4, 1, new Payload_Session()));
            DupObjs.Add(new DupObj(DupObjClass.NET_MessageBroker, 5, 1));
            uint[] IDGeneratorIDs = { 7, 0xD, 0x13, 0x14, 0x15, 0x16, 0x18, 0x1A, 0x23 };
            foreach (uint id in IDGeneratorIDs)
                DupObjs.Add(new DupObj(DupObjClass.IDGenerator, id, 1, new Payload_IDRange(0x1, 0x3FFFFE)));
            DupObjs.Add(new DupObj(DupObjClass.IDGenerator, 0x17, 1, new Payload_IDRange(0x102, 0x3FFFFE)));
            DupObjs.Add(new DupObj(DupObjClass.IDGenerator, 0x19, 1, new Payload_IDRange(0x24, 0x3FFFFE)));
        }

        public static DupObj FindObj(uint handle)
        {
            foreach (DupObj obj in DupObjs)
                if (obj == handle)
                    return obj;
            return null;
        }
    }
}
