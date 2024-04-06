using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseChatService_UnignorePlayer : RMCPResponse
    {
        public GR5_BasicPersona Persona { get; set; }

        public RMCPacketResponseChatService_UnignorePlayer(string name)
        {
            Persona = new GR5_BasicPersona()
            {
                PersonaID = 123456,
                PersonaName = name,
                PersonaStatus = GR5_BasicPersona.STATUS.Offline,
                AvatarPortraitID = 1,
                AvatarDecoratorID = 1,
                AvatarBackgroundColor = 0,
                CurrentCharacterID = 0,
                CurrentCharacterLevel = 13
            };
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Persona.toBuffer(m);
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
