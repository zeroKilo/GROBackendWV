using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class MSG_ID_Net_Obj_Create : BM_Message
    {
        public byte dynamicBankID = 0x2C;
        public byte dynamicBankElementID = 0x15;
        public float[] matrix = new float[16];
        public uint owner = 0x5c0002;
        public byte[] buffer = new byte[8];

        public MSG_ID_Net_Obj_Create()
        {
            msgID = 0x271;
            MemoryStream m = new MemoryStream();
            Helper.WriteU16(m, (ushort)buffer.Length);
            Helper.WriteU8(m, dynamicBankID);
            Helper.WriteU8(m, dynamicBankElementID);
            foreach (float f in matrix)
                Helper.WriteFloat(m, f);
            Helper.WriteU32(m, owner);
            m.Write(buffer, 0, buffer.Length);
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, m.ToArray()));
        }
    }
}
