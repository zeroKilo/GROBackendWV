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
        public byte[] buffer;

        public MSG_ID_Net_Obj_Create()
        {
            msgID = 0x271;
            buffer = MakePayload();
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

        uint handle = new DupObj(DupObjClass.SES_cl_Player_NetZ, 257);
        byte unk1 = 0x55;
        byte[] unk2 = new byte[4];
        byte[] unk3 = new byte[4];

        byte unk4 = 0x66;
        byte[] unk5 = new byte[4];
        byte[] unk6 = new byte[4];
        byte playerLocalIndex = 0x0;
        byte padID = 0x0;
        byte teamID = 0x1;
        uint rdvID = 0x1234;
        uint unk11 = 0x88888888;

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            //Handle
            Helper.WriteU32LE(m, handle);
            //Replica Data 1
            Helper.WriteU8(m, (byte)unk2.Length);
            Helper.WriteU8(m, unk1);
            m.Write(unk2, 0, unk2.Length);
            //subStuff
            Helper.WriteU32(m, 1);
            Helper.WriteU32(m, 2);
            Helper.WriteU32(m, 3);
            Helper.WriteU32(m, 4);
            Helper.WriteU32(m, 4);
            Helper.WriteU32(m, 4);
            Helper.WriteU32(m, 5);
            Helper.WriteU32(m, 6);
            Helper.WriteU32(m, 7);
            Helper.WriteU16(m, 8);
            Helper.WriteU8(m, 1);//class?
            Helper.WriteU16(m, 10);
            Helper.WriteU32(m, 111);
            Helper.WriteU32(m, (uint)unk3.Length);
            m.Write(unk3, 0, unk3.Length);

            //Replica Data 2
            Helper.WriteU8(m, (byte)unk5.Length);
            Helper.WriteU8(m, unk4);
            m.Write(unk2, 0, unk5.Length);
            //subStuff
            //Rest
            Helper.WriteU8(m, playerLocalIndex);
            Helper.WriteU8(m, padID);
            Helper.WriteU8(m, teamID);
            Helper.WriteU32(m, rdvID);
            Helper.WriteU32(m, unk11);
            return m.ToArray();
        }
    }
}
