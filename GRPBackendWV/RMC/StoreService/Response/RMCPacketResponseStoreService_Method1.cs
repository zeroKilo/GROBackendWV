using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseStoreService_Method1 : RMCPacketReply
    {
        public class SKUItem
        {
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public uint unk4;
            public float unk5;
            public float unk6;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteFloat(s, unk5);
                Helper.WriteFloat(s, unk6);
            }
        }

        public class SKU
        {
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public uint unk4;
            public uint unk5;
            public uint unk6;
            public uint unk7;
            public uint unk8;
            public string unk9;
            public uint unk10;
            public List<SKUItem> items = new List<SKUItem>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU32(s, unk2);
                Helper.WriteU32(s, unk3);
                Helper.WriteU32(s, unk4);
                Helper.WriteU32(s, unk5);
                Helper.WriteU32(s, unk6);
                Helper.WriteU32(s, unk7);
                Helper.WriteU32(s, unk8);
                Helper.WriteString(s, unk9);
                Helper.WriteU32(s, unk10);
                Helper.WriteU32(s, (uint)items.Count);
                foreach (SKUItem i in items)
                    i.toBuffer(s);
            }
        }

        public List<SKU> skus = new List<SKU>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)skus.Count);
            foreach (SKU s in skus)
                s.toBuffer(m);
            return m.ToArray(); ;
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStoreService_Method1]";
        }
    }
}
