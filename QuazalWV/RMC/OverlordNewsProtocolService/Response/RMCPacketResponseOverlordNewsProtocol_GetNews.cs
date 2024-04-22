using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace QuazalWV
{
    public class RMCPacketResponseOverlordNewsProtocol_GetNews : RMCPResponse
    {
        public List<GR5_NewsMessage> News { get; set; }

        public RMCPacketResponseOverlordNewsProtocol_GetNews(ClientInfo client, OverlordNewsProtocolService.REQUEST newsType, uint msgId)
        {
            if (client.systemNews.Count == 0)
            {
                client.systemNews.Add(
                    new GR5_NewsMessage(NewsMessageType.WelcomeToGRO, client, msgId, client.PID)
                );
                client.systemNews.Add(
                    new GR5_NewsMessage(NewsMessageType.LevelUp, client, msgId, client.PID, 0, 16)
                );
                client.systemNews.Add(
                    new GR5_NewsMessage(NewsMessageType.WeaponLevelUp, client, msgId, client.PID, 1000, 5)
                );
                client.systemNews.Add(
                    new GR5_NewsMessage(NewsMessageType.MissionCompleted, client, msgId, client.PID, 1)
                );
                client.systemNews.Add(
                    new GR5_NewsMessage(NewsMessageType.AchievementCompleted, client, msgId, client.PID, 1)
                );
                client.systemNews.Add(
                    new GR5_NewsMessage(NewsMessageType.RewardReceived, client, msgId, client.PID, 1)
                );
                client.systemNews.Add(
                    new GR5_NewsMessage(NewsMessageType.AvatarChanged, client, msgId, client.PID, 8, 1)
                );
                client.systemNews.Add(
                    new GR5_NewsMessage(NewsMessageType.WeaponKills, client, msgId, client.PID, 1000, 100)
                );
                client.systemNews.Add(
                    new GR5_NewsMessage(NewsMessageType.WeaponHeadshots, client, msgId, client.PID, 1, 50)
                );
            }

            if (client.personaNews.Count == 0)
            {
                client.personaNews.Add(
                    new GR5_NewsMessage(NewsMessageType.MissionCompleted, client, msgId, client.PID, 1)
                );
                client.personaNews.Add(
                    new GR5_NewsMessage(NewsMessageType.AchievementCompleted, client, msgId, client.PID, 1)
                );
                client.personaNews.Add(
                    new GR5_NewsMessage(NewsMessageType.RewardReceived, client, msgId, client.PID, 1)
                );
            }

            if (client.friendNews.Count == 0)
            {
                client.friendNews.Add(
                    new GR5_NewsMessage(NewsMessageType.AvatarChanged, client, msgId, client.PID, 8, 1)
                );
                client.friendNews.Add(
                    new GR5_NewsMessage(NewsMessageType.WeaponKills, client, msgId, client.PID, 1000, 100)
                );
                client.friendNews.Add(
                    new GR5_NewsMessage(NewsMessageType.WeaponHeadshots, client, msgId, client.PID, 1, 50)
                );
            }

            News = new List<GR5_NewsMessage>();

            switch (newsType)
            {
                case OverlordNewsProtocolService.REQUEST.SystemNews:
                    foreach (GR5_NewsMessage msg in client.systemNews) News.Add(msg);
                    break;
                case OverlordNewsProtocolService.REQUEST.PersonaNews:
                    foreach (GR5_NewsMessage msg in client.personaNews) News.Add(msg);
                    break;
                case OverlordNewsProtocolService.REQUEST.FriendsNews:
                    foreach (GR5_NewsMessage msg in client.friendNews) News.Add(msg);
                    break;
                default:
                    break;
            }
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)News.Count);
            foreach (GR5_NewsMessage n in News)
                n.ToBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[GetNews Response]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
