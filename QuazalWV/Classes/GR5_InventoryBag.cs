using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_InventoryBag
    {
        public uint m_PersonaID;
        public uint m_InventoryBagType;
        public List<GR5_InventoryBagSlot> m_InventoryBagSlotVector = new List<GR5_InventoryBagSlot>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_PersonaID);
            Helper.WriteU32(s, m_InventoryBagType);
            Helper.WriteU32(s, (uint)m_InventoryBagSlotVector.Count);
            foreach (GR5_InventoryBagSlot c in m_InventoryBagSlotVector)
                c.toBuffer(s);
        }
    }
}
