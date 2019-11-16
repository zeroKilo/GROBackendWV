using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSkillsService_GetModifiers : RMCPacketReply
    {
        public class SkillModifier
        {
            public uint m_ModifierID;
            public byte m_ModifierType;
            public byte m_PropertyType;
            public byte m_MethodType;
            public string m_MethodValue;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_ModifierID);
                Helper.WriteU8(s, m_ModifierType);
                Helper.WriteU8(s, m_PropertyType);
                Helper.WriteU8(s, m_MethodType);
                Helper.WriteString(s, m_MethodValue);
            }
        }

        public List<SkillModifier> mods = new List<SkillModifier>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)mods.Count);
            foreach (SkillModifier skm in mods)
                skm.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSkillsService_GetModifiers]";
        }
    }
}
