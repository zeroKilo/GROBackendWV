using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_PersonaArmorTier
    {
        public uint ArmorTierID;
        public List<GR5_ArmorInsertSlot> Inserts = new List<GR5_ArmorInsertSlot>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, ArmorTierID);
            Helper.WriteU32(s, (uint)Inserts.Count);
            foreach (GR5_ArmorInsertSlot a in Inserts)
                a.toBuffer(s);
        }
    }
}
