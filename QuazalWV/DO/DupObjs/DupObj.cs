using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public enum DupObjClass
    {
        DefaultCell = 07,
        SessionClock = 13,
        SES_cl_Player_NetZ = 19,
        SES_cl_RDVInfo_NetZ = 20,
        SES_cl_SessionInfos = 21,
        RootDO = 22,
        Station = 23,
        Session = 24,
        IDGenerator = 25,
        PromotionReferee = 26,
        NET_MessageBroker = 35,
    }

    public class DupObj
    {
        public DupObjPayload Payload;
        public DupObjClass Class;
        public DupObj Master;
        public uint ID;

        public DupObj(uint u)
        {            
            Class = (DupObjClass)(u >> 22);
            ID = u & 0x3FFFFF;
        }

        public DupObj(DupObjClass cls, uint id)
        {
            Class = cls;
            ID = id;
        }

        public DupObj(DupObjClass cls, uint id, uint masterID)
        {
            Class = cls;
            ID = id;
            Master = new DupObj(DupObjClass.Station, masterID);
        }

        public DupObj(DupObjClass cls, uint id, uint masterID, DupObjPayload pl)
        {
            Class = cls;
            ID = id;
            Master = new DupObj(DupObjClass.Station, masterID);
            Payload = pl;
        }

        public static implicit operator uint(DupObj obj)
        {
            return (uint)((byte)obj.Class << 22) | obj.ID;
        }

        public string getDesc()
        {
            return "[ID=" + ID + " Master=" + (Master == null ? 0 : Master.ID) + " " + Class + "]";
        }

        public byte[] getPayload()
        {
            if (Payload != null)
                return Payload.toBuffer();
            else
                return new byte[0];
        }
    }
}
