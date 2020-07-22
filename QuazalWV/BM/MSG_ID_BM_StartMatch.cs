using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public  class MSG_ID_BM_StartMatch : BM_Message
    {
        public MSG_ID_BM_StartMatch()
        {
            msgID = 0x384;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, new byte[4]));
        }
    }
}
