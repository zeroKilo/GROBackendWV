using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class MSG_ID_NetRule_Synchronize : BM_Message
    {
        public uint newState = 3;
        public uint syncro = 0x12345678;
        public uint unk1 = 1;
        public MSG_ID_NetRule_Synchronize()
        {
            msgID = 0x29C;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Integer, (int)newState));
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Integer, (int)syncro));
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Integer, (int)unk1));
        }
    }
}
