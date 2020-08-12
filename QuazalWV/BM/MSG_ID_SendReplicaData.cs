using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class MSG_ID_SendReplicaData : BM_Message
    {
        public MSG_ID_SendReplicaData(byte[] payload)
        {
            msgID = 0x98;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, payload));
        }
    }
}
