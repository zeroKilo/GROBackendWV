using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSurveyService_Method1 : RMCPacketReply
    {
        public class Survey
        {
            public uint unk1;
            public byte unk2;
            public string unk3;
            public string unk4;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteString(s, unk3);
                Helper.WriteString(s, unk4);
            }
        }

        public List<Survey> list = new List<Survey>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (Survey s in list)
                s.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSurveyService_Method1]";
        }
    }
}
