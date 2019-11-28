using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAdvertisementsService_Method2 : RMCPacketReply
    {
        public class AdContainer
        {
            public uint unk1;
            public uint unk2;
            public string unk3;
            public byte unk4;
            public byte unk5;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteString(s, unk3);
                Helper.WriteU8(s, unk4);
                Helper.WriteU8(s, unk5);
            }
        }

        public class AdServer
        {
            public uint unk1;
            public byte unk2;
            public string unk3;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteString(s, unk3);
            }
        }

        public class AdStaticList
        {
            public uint unk1;
            public uint unk2;
            public byte unk3;
            public byte unk4;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU8(s, unk3);
                Helper.WriteU8(s, unk4);
            }
        }

        public class Unknown
        {
            public List<AdStaticList> list = new List<AdStaticList>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, (uint)list.Count);
                foreach (AdStaticList a in list)
                    a.toBuffer(s);
            }
        }

        public class AdRecommender
        {
            public uint unk1;
            public uint unk2;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
            }
        }

        public List<AdContainer> unk1 = new List<AdContainer>();
        public List<AdServer> unk2 = new List<AdServer>();
        public List<Unknown> unk3 = new List<Unknown>();
        public List<AdRecommender> unk4 = new List<AdRecommender>();

        public RMCPacketResponseAdvertisementsService_Method2()
        {
            unk1.Add(new AdContainer());
            unk2.Add(new AdServer());
            Unknown u = new Unknown();
            u.list.Add(new AdStaticList());
            unk3.Add(u);
            unk4.Add(new AdRecommender());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)unk1.Count);
            foreach (AdContainer u in unk1)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)unk2.Count);
            foreach (AdServer u in unk2)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)unk3.Count);
            foreach (Unknown u in unk3)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)unk4.Count);
            foreach (AdRecommender u in unk4)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAdvertisementsService_Method2]";
        }
    }
}
