using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseProgressionService_GetLevels : RMCPResponse
    {
        public List<GR5_Level> levels = new List<GR5_Level>();

        public RMCPacketResponseProgressionService_GetLevels()
        {
            levels.Add(new GR5_Level());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)levels.Count);
            foreach (GR5_Level l in levels)
                l.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseProgressionService_GetLevels]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
