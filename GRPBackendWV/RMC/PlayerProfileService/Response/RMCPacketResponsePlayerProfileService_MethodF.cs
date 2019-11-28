using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePlayerProfileService_MethodF : RMCPacketReply
    {
        public class FaceSkinTone
        {
            public uint id;
            public byte objectType;
            public uint objectKey;
            public uint oasisName;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, id);
                Helper.WriteU8(s, objectType);
                Helper.WriteU32(s, objectKey);
                Helper.WriteU32(s, oasisName);
            }
        }

        public List<FaceSkinTone> list = new List<FaceSkinTone>();

        public RMCPacketResponsePlayerProfileService_MethodF()
        {
            list.Add(new FaceSkinTone());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (FaceSkinTone fst in list)
                fst.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_MethodF]";
        }
    }

}
