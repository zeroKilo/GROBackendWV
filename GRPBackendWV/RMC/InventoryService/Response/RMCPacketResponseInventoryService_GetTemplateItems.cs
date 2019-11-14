using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_GetTemplateItems : RMCPacketReply
    {
        public class TemplateItem
        {
            public uint unk1;
            public byte unk2;
            public string unk3;
            public byte unk4;
            public bool unk5;
            public bool unk6;
            public bool unk7;
            public bool unk8;
            public bool unk9;
            public uint unk10;
            public uint unk11;
            public float unk12;
            public uint unk13;
            public uint unk14;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, unk1);
                Helper.WriteU8(s, unk2);
                Helper.WriteString(s, unk3);
                Helper.WriteU8(s, unk4);
                Helper.WriteBool(s, unk5);
                Helper.WriteBool(s, unk6);
                Helper.WriteBool(s, unk7);
                Helper.WriteBool(s, unk8);
                Helper.WriteBool(s, unk9);
                Helper.WriteU32(s, unk10);
                Helper.WriteU32(s, unk11);
                Helper.WriteFloat(s, unk12);
                Helper.WriteU32(s, unk13);
                Helper.WriteU32(s, unk14);
            }
        }

        public List<TemplateItem> items = new List<TemplateItem>();

        public RMCPacketResponseInventoryService_GetTemplateItems()
        {
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)items.Count);
            foreach (TemplateItem item in items)
                item.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_GetTemplateItems]";
        }
    }
}
