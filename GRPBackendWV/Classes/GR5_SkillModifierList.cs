using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_SkillModifierList
    {
        public uint m_ID;
        public List<uint> m_ModifierIDVector = new List<uint>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, (uint)m_ModifierIDVector.Count);
            foreach (uint u in m_ModifierIDVector)
                Helper.WriteU32(s, u);
        }
    }
}
