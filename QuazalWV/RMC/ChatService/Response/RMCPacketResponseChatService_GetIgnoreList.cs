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
        public List<GR5_BasicPersona> Personas { get; set; }

        public RMCPacketResponseChatService_GetIgnoreList()
        {
            Personas = new List<GR5_BasicPersona>();
            var example = new GR5_BasicPersona()
            {
                PersonaID = 123456,
                PersonaName = "a bad guy",
                PersonaStatus = GR5_BasicPersona.STATUS.Offline,
                AvatarPortraitID = 1,
                AvatarDecoratorID = 1,
                AvatarBackgroundColor = 0,
                CurrentCharacterID = 0,
                CurrentCharacterLevel = 13
            };
            Personas.Add(example);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)Personas.Count);
            foreach (GR5_BasicPersona p in Personas)
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
