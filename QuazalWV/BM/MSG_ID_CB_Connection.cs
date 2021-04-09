using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class MSG_ID_CB_Connection : BM_Message
    {
        List<uint> clientIdList = new List<uint>();

        public MSG_ID_CB_Connection(ClientInfo client)
        {
            msgID = 0x266;
            for (int i = 0; i < 17; i++) clientIdList[i] = client.stationID; //check if 2 is ok or it needs the longer form
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, MakePayload()));
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, 0x80000019);
            Helper.WriteU32(m, (uint)clientIdList.Count);
            foreach(uint id in clientIdList) Helper.WriteU32(m, id);
            return m.ToArray();
        }
    }
}
