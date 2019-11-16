using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseProgressionService_GetLevels : RMCPacketReply
    {
        public class Level
        {
            public uint m_Id;
            public uint m_TotalPEC;
            public uint m_Level;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_Id);
                Helper.WriteU32(s, m_TotalPEC);
                Helper.WriteU32(s, m_Level);
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
            return "[RMCPacketResponseProgressionService_GetLevels]";
        }
    }
}
