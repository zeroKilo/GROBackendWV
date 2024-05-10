using System.IO;

namespace QuazalWV
{
    public class GR5_AdStaticList
    {
        public uint m_AdServerId;
        public uint m_AdvertId;
        public byte m_AdType;
        public byte m_Priority;

        public void ToBuffer(Stream s)
        {
            Helper.WriteU32(s, m_AdServerId);
            Helper.WriteU32(s, m_AdvertId);
            Helper.WriteU8(s, m_AdType);
            Helper.WriteU8(s, m_Priority);
        }
    }
}
