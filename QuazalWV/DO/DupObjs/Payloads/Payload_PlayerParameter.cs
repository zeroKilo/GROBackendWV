using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_PlayerParameter : DupObjPayload
    {
        public uint pid;
        public uint matchID;
        public uint handleDO;
        public BitFieldU32 bitField0C;
        public BitFieldU32 bitField10;
        public BitFieldU32 bitField14;
        public uint dword18;
        public uint dword1C;
        public uint dword20;
        public byte[] padding;

        public Payload_PlayerParameter(byte[] buff)
        {
            MemoryStream m = new MemoryStream(buff);
            m.Seek(0, 0);
            pid = Helper.ReadU32(m);
            matchID = Helper.ReadU32(m);
            handleDO = Helper.ReadU32(m);
            bitField0C = new BitFieldU32(
                new List<BitFieldU32.BitFieldEntry>()
                {
                    new BitFieldU32.BitFieldEntry(0, 12, "GhostRank"),  //0
                    new BitFieldU32.BitFieldEntry(12, 8, "PlayerLevel"),//1
                    new BitFieldU32.BitFieldEntry(20, 8, "gap_0"),      //2
                    new BitFieldU32.BitFieldEntry(28, 4, "Reserved")    //3
                }, 
                Helper.ReadU32(m)
            );
            bitField10 = new BitFieldU32(
                new List<BitFieldU32.BitFieldEntry>()
                {
                    new BitFieldU32.BitFieldEntry(0, 8, "gap_0"),         //0
                    new BitFieldU32.BitFieldEntry(8, 5, "PlayerClass"),   //1
                    new BitFieldU32.BitFieldEntry(13, 4, "TeamIndex"),    //2
                    new BitFieldU32.BitFieldEntry(17, 7, "gap_1"),        //3
                    new BitFieldU32.BitFieldEntry(24, 1, "ChangeState"),  //4
                    new BitFieldU32.BitFieldEntry(25, 1, "IsSynced"),     //5
                    new BitFieldU32.BitFieldEntry(26, 1, "FetchRequest"), //6
                    new BitFieldU32.BitFieldEntry(27, 5, "Reserved"),     //7
                },
                Helper.ReadU32(m)
            );
            bitField14 = new BitFieldU32(
                new List<BitFieldU32.BitFieldEntry>()
                {
                    new BitFieldU32.BitFieldEntry(0, 7, "SpawnCount"),   //0
                    new BitFieldU32.BitFieldEntry(7, 1, "RequestSpawn"), //1
                    new BitFieldU32.BitFieldEntry(8, 4, "AMMStatus"),    //2
                    new BitFieldU32.BitFieldEntry(12, 1, "ClientReady"), //3
                    new BitFieldU32.BitFieldEntry(13, 1, "ServerReady"), //4
                    new BitFieldU32.BitFieldEntry(14, 18, "Reserved"),   //5
                },
                Helper.ReadU32(m)
            );
            dword18 = Helper.ReadU32(m);
            dword1C = Helper.ReadU32(m);
            dword20 = Helper.ReadU32(m);
            padding = new byte[0x1C];
            m.Read(padding, 0, 0x1C);
        }

        public override byte[] toBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, pid);
            Helper.WriteU32(m, matchID);
            Helper.WriteU32(m, handleDO);
            Helper.WriteU32(m, bitField0C.ToU32());
            Helper.WriteU32(m, bitField10.ToU32());
            Helper.WriteU32(m, bitField14.ToU32());
            Helper.WriteU32(m, dword18);
            Helper.WriteU32(m, dword1C);
            Helper.WriteU32(m, dword20);
            m.Write(padding, 0, 0x1C);
            return m.ToArray();
        }

        public override string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "PID        = 0x" + pid.ToString("X8"));
            sb.AppendLine(tabs + "MatchID    = 0x" + matchID.ToString("X8"));
            sb.AppendLine(tabs + "HandleDO   = 0x" + handleDO.ToString("X8"));
            sb.AppendLine(tabs + "Bitfield0C = (0x" + bitField0C.ToU32().ToString("X8") + ")");
            foreach(BitFieldU32.BitFieldEntry e in bitField0C.entries)
                sb.AppendLine(tabs + "\t" + e.name.PadRight(20) + " : " + e.size.ToString("D2") + " = " + e.word);
            sb.AppendLine(tabs + "Bitfield10 = (0x" + bitField10.ToU32().ToString("X8") + ")");
            foreach (BitFieldU32.BitFieldEntry e in bitField10.entries)
                sb.AppendLine(tabs + "\t" + e.name.PadRight(20) + " : " + e.size.ToString("D2") + " = " + e.word);
            sb.AppendLine(tabs + "Bitfield14 = (0x" + bitField14.ToU32().ToString("X8") + ")");
            foreach (BitFieldU32.BitFieldEntry e in bitField14.entries)
                sb.AppendLine(tabs + "\t" + e.name.PadRight(20) + " : " + e.size.ToString("D2") + " = " + e.word);
            sb.AppendLine(tabs + "DWORD18    = 0x" + dword18.ToString("X8"));
            sb.AppendLine(tabs + "DWORD1C    = 0x" + dword1C.ToString("X8"));
            sb.AppendLine(tabs + "DWORD20    = 0x" + dword20.ToString("X8"));
            sb.Append(tabs + "Rest       = ");
            foreach (byte b in padding)
                sb.Append(b.ToString("X2") + " ");
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
