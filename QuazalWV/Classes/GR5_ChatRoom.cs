using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_ChatRoom
    {
        public enum LANGUAGE
        {
            English,
            Japanese,
            German,
            French,
            Spanish,
            Italian,
            Korean,
            Chinese,
            Polish
        }

        public GR5_Gathering Gathering { get; set; }
        public string RoomName { get; set; }
        public byte RoomType { get; set; }
        public LANGUAGE RoomLanguage { get; set; }
        public byte RoomNumber { get; set; }

        public GR5_ChatRoom()
        {
            Gathering = new GR5_Gathering()
            {
                m_idMyself = 1,
                m_pidHost = 0x1234,
                m_pidOwner = 0x1234,
                m_uiMinParticipants = 1,
                m_uiMaxParticipants = 2,
                m_uiParticipationPolicy = 1,
                m_uiPolicyArgument = 20,
                m_uiFlags = 1,
                m_uiState = 0,
                m_strDescription = "mimak"
            };
            RoomName = "";
            RoomLanguage = LANGUAGE.English;
            RoomNumber = 0;
        }

        public void ToBuffer(Stream s)
        {
            Gathering.toBuffer(s);
            Helper.WriteString(s, RoomName);
            Helper.WriteU8(s, RoomType);
            Helper.WriteU8(s, (byte)RoomLanguage);
            Helper.WriteU8(s, RoomNumber);
        }
    }
}
