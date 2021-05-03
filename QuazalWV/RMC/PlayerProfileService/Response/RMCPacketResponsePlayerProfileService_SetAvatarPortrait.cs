using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponsePlayerProfileService_SetAvatarPortrait : RMCPResponse
    {
        public uint portraitId;
        public uint backgroundColor;

        public RMCPacketResponsePlayerProfileService_SetAvatarPortrait(ClientInfo client, uint newPortrait, uint newColor)
        {
            portraitId = newPortrait;
            backgroundColor = newColor;
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, portraitId);
            Helper.WriteU32(m, backgroundColor);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_SetAvatarPortrait]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
