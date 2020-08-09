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
        public MSG_ID_Entity_Cmd(byte cmdID)
        {
            msgID = 0x96;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, MakePayload(cmdID)));
        }

        public byte[] MakePayload(byte cmdID)
        {
            MemoryStream m = new MemoryStream();
            BitBuffer buf = new BitBuffer();
            buf.WriteBits(0x1, 32);         //handle
            buf.WriteBits(cmdID, 6);         //cmd1
            buf.WriteBits(0x0, 1);          //flag1
            buf.WriteBits(0x0, 1);          //flags2
            switch (cmdID)
            {
                case 0x33:
                    buf.WriteBits(0x2, 4);          //state
                    break;
                case 0x34:
                    break;
            }
            byte[] data = buf.toArray();
            m.Write(data, 0, data.Length);
            return m.ToArray();
        }
    }
}
