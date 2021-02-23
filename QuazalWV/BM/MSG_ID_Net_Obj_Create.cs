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
        public uint owner = 0x5c00002;

        public MSG_ID_Net_Obj_Create(byte bank, byte element, byte[] payload)
        {
            msgID = 0x271;
            MemoryStream m = new MemoryStream();
            Helper.WriteU16(m, (ushort)payload.Length);
            Helper.WriteU8(m, bank);
            Helper.WriteU8(m, element);
            matrix[0] = 1;
            matrix[5] = 1;
            matrix[10] = 1;
            //spawn position
            matrix[12] = 0; //x
            matrix[13] = 0; //y
            matrix[14] = 0; //z
            matrix[15] = 1;
            foreach (float f in matrix)
                Helper.WriteFloat(m, f);
            Helper.WriteU32(m, owner);
            m.Write(payload, 0, payload.Length);
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, m.ToArray()));
        }
    }
}
