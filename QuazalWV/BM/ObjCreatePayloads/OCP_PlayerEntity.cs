using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class OCP_PlayerEntity
    {
        public uint handle;
        public byte unk1 = 0x56;
        public byte[] unk2 = new byte[4];
        public byte[] unk3 = new byte[4];
        public byte unk4 = 0x67;
        public byte[] unk5 = new byte[4];
        public byte[] unk6 = new byte[4];
        public byte playerLocalIndex = 0x0;
        public byte padID = 0x0;
        public byte teamID = 0x1;
        public uint rdvID = 0x1234;
        public uint unk11 = 0x99999999;

        public OCP_PlayerEntity(uint h)
        {
            handle = h;
        }

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
            Helper.WriteU32(m, 2);
            Helper.WriteU32(m, 3);
            Helper.WriteU32(m, 4);
            Helper.WriteU8(m, 5);

            Helper.WriteU8(m, 6);
            Helper.WriteU8(m, 7);
            Helper.WriteU8(m, 8);
            Helper.WriteU8(m, 9);

            Helper.WriteU8(m, 0);//count
            Helper.WriteU8(m, 10);
            Helper.WriteU32(m, 11);
            Helper.WriteU16(m, 12);

            Helper.WriteU16(m, 13);
            byte[] tmp = Helper.MakeFilledArray(9);
            m.Write(tmp, 0, tmp.Length);
            Helper.WriteU32(m, 14);
            Helper.WriteU32(m, 15);

            Helper.WriteU8(m, 16);
            tmp = Helper.MakeFilledArray(12);
            m.Write(tmp, 0, tmp.Length);
            Helper.WriteU8(m, 17);
            Helper.WriteU16(m, 18);

            Helper.WriteU16(m, 19);
            Helper.WriteU8(m, 20);
            Helper.WriteU8(m, 21);
            tmp = Helper.MakeFilledArray(13);
            m.Write(tmp, 0, tmp.Length);

            Helper.WriteU8(m, 22);
            Helper.WriteU8(m, 23);
            tmp = Helper.MakeFilledArray(5);
            m.Write(tmp, 0, tmp.Length);
            Helper.WriteU16(m, 24);

            Helper.WriteU16(m, 25);
            Helper.WriteU32(m, 26);
            Helper.WriteU8(m, 27);
            Helper.WriteU8(m, 28);
            Helper.WriteU8(m, 29);

            //Replica Data 2
            Helper.WriteU8(m, (byte)unk5.Length);
            Helper.WriteU8(m, unk4);
            m.Write(unk2, 0, unk5.Length);
            //subStuff
            tmp = Helper.MakeFilledArray(8);
            m.Write(tmp, 0, tmp.Length);
            Helper.WriteU16(m, 3);
            tmp = Helper.MakeFilledArray(8);
            m.Write(tmp, 0, tmp.Length);

            Helper.WriteU8(m, 4);
            Helper.WriteU16(m, 5);

            //Rest
            Helper.WriteU8(m, teamID);
            Helper.WriteU8(m, 0x0); //classID
            Helper.WriteU32(m, rdvID);
            Helper.WriteU32(m, unk11);
            Helper.WriteU8(m, 0x22);
            Helper.WriteU8(m, 0x33);
            Helper.WriteU8(m, 0x44);
            for (int i = 0; i < 9; i++)
                Helper.WriteU8(m, 0);
            Helper.WriteU32(m, 0x55);

            return m.ToArray();
        }
    }
}
