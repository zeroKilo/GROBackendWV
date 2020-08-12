using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class MSG_ID_ReceiveReplicaData : BM_Message
    {
        public MSG_ID_ReceiveReplicaData()
        {
            msgID = 0x99;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, MakePayload()));
        }

        public byte[] MakePayload()
        {
            return new byte[] { 0x01, 0x15, 0x00, 0x00, 0x00, 0x00, 0x02, 0x01, 0x05, 0x1B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x31, 0x80, 0x75, 0x06, 0x04, 0xE5, 0x02 };
        }
    }
}
