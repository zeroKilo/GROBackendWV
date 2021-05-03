using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponsePlayerProfileService_SetAvatarDecorator : RMCPResponse
    {
        public uint decoratorId;

        public RMCPacketResponsePlayerProfileService_SetAvatarDecorator(ClientInfo client, uint decoId)
        {
            decoratorId = decoId;
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, decoratorId);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_SetAvatarDecorator]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
