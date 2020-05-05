using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_Invitee
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
}
