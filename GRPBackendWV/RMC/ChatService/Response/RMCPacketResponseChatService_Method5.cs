using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseChatService_Method5 : RMCPResponse
    {
        public GR5_Gathering unk1 = new GR5_Gathering();
        public string unk2;
        public byte unk3;
        public byte unk4;
        public byte unk5;

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            unk1.toBuffer(m);
            Helper.WriteString(m, unk2);
            Helper.WriteU8(m, unk3);
            Helper.WriteU8(m, unk4);
            Helper.WriteU8(m, unk5);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseChatService_Method5]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
