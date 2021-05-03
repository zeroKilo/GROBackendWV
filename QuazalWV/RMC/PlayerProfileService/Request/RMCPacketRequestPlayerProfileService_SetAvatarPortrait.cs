using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestPlayerProfileService_SetAvatarPortrait : RMCPRequest
    {
        public uint portraitId;
        public uint backgroundColor;

        public RMCPacketRequestPlayerProfileService_SetAvatarPortrait(Stream s)
        {
            portraitId = Helper.ReadU32(s);
            backgroundColor = Helper.ReadU32(s);
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[New portrait: " + portraitId + ", background color: " + backgroundColor + "]");
            return "";
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[SetAvatarPortrait Request: portrait=" + portraitId + ", background color: " + backgroundColor + "]";
        }
    }
}
