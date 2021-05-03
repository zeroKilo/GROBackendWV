using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestPlayerProfileService_SetAvatarDecorator : RMCPRequest
    {
        public uint decoratorId;

        public RMCPacketRequestPlayerProfileService_SetAvatarDecorator(Stream s)
        {
            decoratorId = Helper.ReadU32(s);
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[New decorator: " + decoratorId + "]");
            return "";
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[SetAvatarDecorator Request: decorator=" + decoratorId + "]";
        }
    }
}
