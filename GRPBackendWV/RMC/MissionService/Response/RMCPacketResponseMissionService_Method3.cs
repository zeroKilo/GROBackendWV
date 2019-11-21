using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseMissionService_Method3 : RMCPacketReply
    {
        public class PersonaMission
        {
            public uint mMissionSequenceId;
            public byte mMissionStatus;
            public uint mStartDateTime;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, mMissionSequenceId);
                Helper.WriteU8(s, mMissionStatus);
                Helper.WriteU32(s, mStartDateTime);
            }
        }

        public List<PersonaMission> list = new List<PersonaMission>();
        public uint unk1;
        public uint unk2;
        public uint unk3;
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (PersonaMission pm in list)
                pm.toBuffer(m);
            Helper.WriteU32(m, unk1);
            Helper.WriteU32(m, unk2);
            Helper.WriteU32(m, unk3);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseMissionService_Method3]";
        }
    }
}
