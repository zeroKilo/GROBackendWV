using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseChatService_GetIgnoreList : RMCPResponse
    {
        public List<GR5_BasicPersona> personas = new List<GR5_BasicPersona>();

        public RMCPacketResponseChatService_GetIgnoreList()
        {
            personas.Add(new GR5_BasicPersona());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)personas.Count);
            foreach (GR5_BasicPersona p in personas)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseChatService_GetIgnoreList]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
