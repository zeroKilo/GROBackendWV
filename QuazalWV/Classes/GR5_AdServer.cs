using System.IO;

namespace QuazalWV
{
    public class GR5_AdServer
    {
        public uint m_Id;
        public byte m_Type;
        public string m_DesignerName;

        public void ToBuffer(Stream s)
        {
            Helper.WriteU32(s, m_Id);
            Helper.WriteU8(s, m_Type);
            Helper.WriteString(s, m_DesignerName);
        }
    }
}
