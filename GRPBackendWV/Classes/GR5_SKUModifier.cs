using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_SKUModifier
    {
        public uint m_ID;
        public uint m_CouponBatchID;
        public uint m_TimeStart;
        public uint m_TimeExpired;
        public uint m_TargetType;
        public uint m_TargetValue;
        public string m_Tag;
        public List<GR5_SKUModifierCondition> m_ConditionVector = new List<GR5_SKUModifierCondition>();
        public List<GR5_SKUModifierOutput> m_OutputVector = new List<GR5_SKUModifierOutput>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_CouponBatchID);
            Helper.WriteU32(s, m_TimeStart);
            Helper.WriteU32(s, m_TimeExpired);
            Helper.WriteU32(s, m_TargetType);
            Helper.WriteU32(s, m_TargetValue);
            Helper.WriteString(s, m_Tag);
            Helper.WriteU32(s, (uint)m_ConditionVector.Count);
            foreach (GR5_SKUModifierCondition c in m_ConditionVector)
                c.toBuffer(s);
            Helper.WriteU32(s, (uint)m_OutputVector.Count);
            foreach (GR5_SKUModifierOutput c in m_OutputVector)
                c.toBuffer(s);
        }
    }
}
