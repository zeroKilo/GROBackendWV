using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePlayerProfileService_Method11 : RMCPacketReply
    {
        public class Notification
        {
            public uint m_MajorType;
            public uint m_MinorType;
            public uint m_Param1;
            public uint m_Param2;
            public string m_String;
            public uint m_Param3;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_MajorType);
                Helper.WriteU32(s, m_MinorType);
                Helper.WriteU32(s, m_Param1);
                Helper.WriteU32(s, m_Param2);
                Helper.WriteString(s, m_String);
                Helper.WriteU32(s, m_Param3);
            }
        }

        public List<Notification> list = new List<Notification>();

        public RMCPacketResponsePlayerProfileService_Method11()
        {
            list.Add(new Notification());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (Notification n in list)
                n.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_Method11]";
        }
    }

}
