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
            matrix[15] = 1;
            foreach (float f in matrix)
                Helper.WriteFloat(m, f);
            Helper.WriteU32(m, owner);
            m.Write(payload, 0, payload.Length);
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, m.ToArray()));
        }

        static uint handle;
        static byte unk1 = 0x55;
        static byte[] unk2 = new byte[4];
        static byte[] unk3 = new byte[4];

        static byte unk4 = 0x66;
        static byte[] unk5 = new byte[4];
        static byte[] unk6 = new byte[4];
        static byte playerLocalIndex = 0x0;
        static byte padID = 0x0;
        static byte teamID = 0x1;
        static uint rdvID = 0x1234;
        static uint unk11 = 0x88888888;

        public static byte[] MakePayload1(uint h)
        {
            handle = h;
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

        public static byte[] MakePayload2(uint h)
        {
            handle = h;
            MemoryStream m = new MemoryStream();
            //Handle
            Helper.WriteU32LE(m, handle);

            //Replica Data 1
            Helper.WriteU8(m, (byte)unk2.Length);
            Helper.WriteU8(m, unk1);
            m.Write(unk2, 0, unk2.Length);
            //subStuff
            for (int i = 0; i < 23; i++)
                Helper.WriteU32(m, 1);
            byte[] tmp = new byte[13];
            m.Write(tmp, 0, 13);
            for (int i = 0; i < 9; i++)
                Helper.WriteU32(m, 3);

            //Replica Data 2
            Helper.WriteU8(m, (byte)unk5.Length);
            Helper.WriteU8(m, unk4);
            m.Write(unk2, 0, unk5.Length);
            //subStuff
            for (int i = 0; i < 5; i++)
                Helper.WriteU32(m, 4);

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
