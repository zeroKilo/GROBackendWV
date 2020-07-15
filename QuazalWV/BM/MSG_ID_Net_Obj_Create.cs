using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class MSG_ID_Net_Obj_Create : BM_Message
    {
        public MSG_ID_Net_Obj_Create()
        {
            msgID = 0x271;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, new byte[8]));
        }
    }
}
