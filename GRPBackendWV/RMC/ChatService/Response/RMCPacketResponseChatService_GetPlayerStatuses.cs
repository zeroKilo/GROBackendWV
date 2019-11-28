using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseChatService_GetPlayerStatuses : RMCPacketReply
    {
        public class BasicPersona
        {
            public uint PersonaID;
            public string PersonaName;
            public byte PersonaStatus;
            public uint AvatarPortraitID;
            public uint AvatarDecoratorID;
            public uint AvatarBackgroundColor;
            public byte CurrentCharacterID;
            public byte CurrentCharacterLevel;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, PersonaID);
                Helper.WriteString(s, PersonaName);
                Helper.WriteU8(s, PersonaStatus);
                Helper.WriteU32(s, AvatarPortraitID);
                Helper.WriteU32(s, AvatarDecoratorID);
                Helper.WriteU32(s, AvatarBackgroundColor);
                Helper.WriteU8(s, CurrentCharacterID);
                Helper.WriteU8(s, CurrentCharacterLevel);
            }
        }

        public List<BasicPersona> personas = new List<BasicPersona>();

        public RMCPacketResponseChatService_GetPlayerStatuses()
        {
            personas.Add(new BasicPersona());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)personas.Count);
            foreach (BasicPersona p in personas)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseChatService_GetPlayerStatuses]";
        }
    }
}
