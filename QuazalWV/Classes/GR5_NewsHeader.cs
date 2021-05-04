using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_NewsHeader
    {
        public uint m_ID;
        public uint m_recipientID;
        public uint m_recipientType;
        public uint m_publisherPID;
        public string m_publisherName;
        public ulong m_publicationTime;
        public ulong m_displayTime;
        public ulong m_expirationTime;
        public string m_title;
        public string m_link;

        public GR5_NewsHeader() {}

        public GR5_NewsHeader(ClientInfo publisher, uint id, uint recipentPid)
        {
            m_ID = id;
            m_recipientID = recipentPid;
            m_recipientType = 1;
            m_publisherPID = publisher.PID;
            m_publisherName = publisher.name;
            m_publicationTime = (ulong)DateTime.UtcNow.Ticks;
            m_displayTime = m_publicationTime;
            m_expirationTime = (ulong)DateTime.UtcNow.AddDays(7).Ticks;
            m_title = "Phoenix News";
            m_link = "https://phoenixnetwork.net/";
        }

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_ID);
            Helper.WriteU32(s, m_recipientID);
            Helper.WriteU32(s, m_recipientType);
            Helper.WriteU32(s, m_publisherPID);
            Helper.WriteString(s, m_publisherName);
            Helper.WriteU64(s, m_publicationTime);
            Helper.WriteU64(s, m_displayTime);
            Helper.WriteU64(s, m_expirationTime);
            Helper.WriteString(s, m_title);
            Helper.WriteString(s, m_link);
        }
    }
}
