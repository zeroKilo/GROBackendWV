using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public  class MSG_ID_BM_StartRound : BM_Message
    {
        public MSG_ID_BM_StartRound()
        {
            msgID = 0x384;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, MakePayload()));
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteFloat(m, 111);    //startTime
            Helper.WriteU8(m, 2);       //roundID
            Helper.WriteFloat(m, 0);    //roundStartTime
            Helper.WriteFloat(m, 9999);    //roundDuration
            Helper.WriteU8(m, 0);       //bContested
            Helper.WriteU8(m, 0);       //bIsCurrRoundLast
            return m.ToArray();
        }
    }
}
