using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class MSG_ID_Entity_Cmd : BM_Message
    {
        public MSG_ID_Entity_Cmd()
        {
            msgID = 0x96;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, MakePayload()));
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            //TODO
            return m.ToArray();
        }
    }
}
