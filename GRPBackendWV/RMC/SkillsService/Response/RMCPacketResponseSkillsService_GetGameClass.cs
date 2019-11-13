using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSkillsService_GetGameClass : RMCPacketReply
    {
        public class GameClass
        {
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public string unk4;
            public List<uint> unk5 = new List<uint>();
            public List<uint> unk6 = new List<uint>();
            public uint unk7;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteString(s, unk4);
                Helper.WriteU32(s, (uint)unk5.Count);
                foreach (uint u in unk5)
                    Helper.WriteU32(s, u);
                Helper.WriteU32(s, (uint)unk6.Count);
                foreach (uint u in unk6)
                    Helper.WriteU32(s, u);
                Helper.WriteU32(s, unk7);
            }
        }

        public List<GameClass> classes = new List<GameClass>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)classes.Count);
            foreach (GameClass g in classes)
                g.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetGameClass]";
        }
    }
}
