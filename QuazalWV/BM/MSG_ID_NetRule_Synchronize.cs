using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class MSG_ID_NetRule_Synchronize : BM_Message
    {
        public MSG_ID_NetRule_Synchronize()
        {
            msgID = 0x29C;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Integer, 3));
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Integer, 0x12345678));
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Integer, 1));
        }
    }
}
