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
            BitBuffer buf = new BitBuffer();
            buf.WriteBits(0x4C00101, 32);   //handle
            buf.WriteBits(0x33, 6);         //cmd1
            buf.WriteBits(0x0, 2);
            buf.WriteBits(0x3, 4);          //state
            byte[] data = buf.toArray();
            m.Write(data, 0, data.Length);
            return m.ToArray();
        }
    }
}
