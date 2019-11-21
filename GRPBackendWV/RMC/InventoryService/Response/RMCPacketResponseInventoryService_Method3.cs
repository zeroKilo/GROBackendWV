using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_Method3 : RMCPacketReply
    {
        public class Consumable
        {
            public uint m_ItemID;
            public uint m_AssetKey;
            public uint m_Type;
            public uint m_Value1;
            public uint m_Value2;
            public string m_Name;

            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_ItemID);
                Helper.WriteU32(s, m_AssetKey);
                Helper.WriteU32(s, m_Type);
                Helper.WriteU32(s, m_Value1);
                Helper.WriteU32(s, m_Value2);
                Helper.WriteString(s, m_Name);
            }
        }

        public List<Consumable> cons = new List<Consumable>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)cons.Count);
            foreach (Consumable c in cons)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_Method3]";
        }
    }
}
