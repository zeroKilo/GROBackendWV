using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace QuazalWV
{
    public class RMCPacketResponseOverlordNewsProtocol_GetNews : RMCPResponse
    {
        public List<GR5_NewsMessage> news = new List<GR5_NewsMessage>();

        public RMCPacketResponseOverlordNewsProtocol_GetNews(ClientInfo client, OverlordNewsProtocolService.REQUEST newsType)
        {
            string msgBody;
            switch(newsType)
            {
                case OverlordNewsProtocolService.REQUEST.SystemNews:
                    msgBody = GetWelcomeMsg();
                    break;
                case OverlordNewsProtocolService.REQUEST.PersonaNews:
                    msgBody = GetCompletedMissionMsg();
                    break;
                case OverlordNewsProtocolService.REQUEST.FriendsNews:
                    msgBody = GetWelcomeMsg();
                    break;
                default:
                    msgBody = GetWelcomeMsg();
                    break;
            }
            //i dont think news should be in the database
            news = DBHelper.GetNews(client.PID, msgBody);
        }

        public string GetWelcomeMsg()
        {
            //XML format, see wiki for details
            //it might be possible that you can have multiple message tags here
            //i guess that's why there are all the range validations etc. in rdv.dll
            //TODO: time format isnt valid
            return
                new XElement("news",
                    new XElement("message",
                        //core attributes
                        new XAttribute("unkattr", "somevalue"),
                        new XAttribute("unkattrii", "somevalueii"),
                        new XAttribute("type", 73498),
                        new XAttribute("icon", 2),
                        new XAttribute("oasis", 73498),
                        new XAttribute("pid", 4660),
                        new XAttribute("time", (uint)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds)
                        )
                    ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public string GetCompletedMissionMsg()
        {
            return
                new XElement("news",
                    new XElement("message",
                        new XAttribute("unkattr", "somevalue"),
                        new XAttribute("unkattrii", "somevalueii"),
                        new XAttribute("type", 73502),
                        new XAttribute("icon", 1),
                        new XAttribute("oasis", 73502),
                        new XAttribute("pid", 4660),
                        new XAttribute("time", (uint)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds),
                        //optional message-specific attributes
                        new XAttribute("missionid", 1)
                        )
                    ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)news.Count);
            foreach (GR5_NewsMessage n in news)
                n.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseOverlordNewsProtocol_GetNews]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
