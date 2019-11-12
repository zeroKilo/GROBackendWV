using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseProgressionService_Method1 : RMCPacketReply
    {
        public class Level
        {
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
            }
        }

        public List<Level> levels = new List<Level>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)levels.Count);
            foreach (Level l in levels)
                l.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseProgressionService_Method1]";
        }
    }
}
