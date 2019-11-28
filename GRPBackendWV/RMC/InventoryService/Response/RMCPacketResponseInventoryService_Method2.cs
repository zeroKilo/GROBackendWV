using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseInventoryService_Method2 : RMCPacketReply
    {
        public class Boost
        {
            public uint m_ItemID;
            public uint m_AssetKey;
            public uint m_ModifierList;
            public uint m_Type;
            public string m_Name;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_ItemID);
                Helper.WriteU32(s, m_AssetKey);
                Helper.WriteU32(s, m_ModifierList);
                Helper.WriteU32(s, m_Type);
                Helper.WriteString(s, m_Name);
            }
        }

        public List<Boost> boosts = new List<Boost>();

        public RMCPacketResponseInventoryService_Method2()
        {
            boosts.Add(new Boost());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)boosts.Count);
            foreach (Boost b in boosts)
                b.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseInventoryService_Method2]";
        }
    }
}
