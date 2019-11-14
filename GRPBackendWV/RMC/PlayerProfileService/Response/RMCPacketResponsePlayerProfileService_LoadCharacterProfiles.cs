using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePlayerProfileService_LoadCharacterProfiles : RMCPacketReply
    {
        public class Character
        {
            public uint PersonaID;
            public uint unk2;
            public uint unk3;
            public uint unk4;
            public uint unk5;
            public uint unk6;
            public uint unk7;
            public byte unk8;
            public byte unk9;
            public byte unk10;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, PersonaID);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteU32(s, unk7);
                Helper.WriteU8(s, unk8);
                Helper.WriteU8(s, unk9);
                Helper.WriteU8(s, unk10);
            }
        }

        public uint PersonaID;
        public string PlayerName;
        public uint unk3;
        public uint unk4;
        public uint unk5;
        public uint unk6;
        public uint unk7;
        public uint unk8;
        public byte unk9;
        public uint unk10;
        public uint unk11;
        public uint GhostRank;
        public uint unk13;
        public List<Character> chars = new List<Character>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, PersonaID);
            Helper.WriteString(m, PlayerName);
            Helper.WriteU32(m, unk3);
            Helper.WriteU32(m, unk4);
            Helper.WriteU32(m, unk5);
            Helper.WriteU32(m, unk6);
            Helper.WriteU32(m, unk7);
            Helper.WriteU32(m, unk8);
            Helper.WriteU8(m, unk9);
            Helper.WriteU32(m, unk10);
            Helper.WriteU32(m, unk11);
            Helper.WriteU32(m, GhostRank);
            Helper.WriteU32(m, unk13);
            Helper.WriteU32(m, (uint)chars.Count);
            foreach (Character c in chars)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_LoadCharacterProfiles]";
        }
    }
}
