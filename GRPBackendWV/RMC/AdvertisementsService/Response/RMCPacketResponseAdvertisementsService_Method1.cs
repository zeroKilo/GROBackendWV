using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAdvertisementsService_Method1 : RMCPacketReply
    {
        public class Advertisement
        {
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public byte unk4;
            public byte unk5;
            public uint unk6;
            public string unk7;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU8(s, unk4);
                Helper.WriteU8(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteString(s, unk7);
            }
        }

        public List<Advertisement> ads = new List<Advertisement>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)ads.Count);
            foreach (Advertisement a in ads)
                a.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAdvertisementsService_Method1]";
        }
    }
}
