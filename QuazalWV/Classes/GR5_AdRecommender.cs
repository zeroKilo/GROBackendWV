using System.IO;

namespace QuazalWV
{
    public class GR5_AdRecommender
    {
        public uint m_AdServerId;
        public uint m_AdCount;

        public void ToBuffer(Stream s)
        {
            Helper.WriteU32(s, m_AdServerId);
            Helper.WriteU32(s, m_AdCount);
        }
    }
}
