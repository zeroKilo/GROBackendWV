using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_Coupon
    {
        public uint m_ID;
        public uint m_SKUModifierID;
        public uint m_TimeStart;
        public uint m_TimeExpired;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_SKUModifierID);
            Helper.WriteU32(s, m_TimeStart);
            Helper.WriteU32(s, m_TimeExpired);
        }
    }
}
