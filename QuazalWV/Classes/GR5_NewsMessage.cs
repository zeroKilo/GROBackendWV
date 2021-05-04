using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuazalWV
{
    /// <summary>
    /// News message type based on its oasis id
    /// </summary>
    public enum NewsMessageType
    {
        WelcomeToGRO = 73498,
        AvatarChanged = 73499,
        LevelUp = 73500,
        AchievementCompleted = 73501,
        MissionCompleted = 73502,
        WeaponLevelUp = 73503,
        Kills = 73504,
        Headshots = 73505,
        RewardReceived = 73506
    }

    public class GR5_NewsMessage
    {
        public NewsMessageType type = NewsMessageType.WelcomeToGRO;
        public GR5_NewsHeader header;
        public string m_body;

        public GR5_NewsMessage()
        {
            header = new GR5_NewsHeader();
        }

        public GR5_NewsMessage(NewsMessageType t, ClientInfo publisher, uint id, uint recipentPid, uint arg1 = 0, uint arg2 = 0)
        {
            type = t;
            header = new GR5_NewsHeader(publisher, id, recipentPid);

            switch(type)
            {
                case NewsMessageType.WelcomeToGRO:
                    m_body = GetWelcomeMsg(publisher.PID);
                    break;
                case NewsMessageType.AvatarChanged:
                    m_body = GetAvatarChangedMsg(arg1, arg2);
                    break;
                case NewsMessageType.LevelUp:
                    m_body = "";
                    break;
                case NewsMessageType.AchievementCompleted:
                    m_body = "";
                    break;
                case NewsMessageType.MissionCompleted:
                    m_body = GetMissionCompletedMsg(publisher.PID, arg1);
                    break;
                case NewsMessageType.WeaponLevelUp:
                    m_body = "";
                    break;
                case NewsMessageType.Kills:
                    m_body = "";
                    break;
                case NewsMessageType.Headshots:
                    m_body = "";
                    break;
                case NewsMessageType.RewardReceived:
                    m_body = "";
                    break;
            }
        }

        //TODO: time format isnt valid
        public string GetWelcomeMsg(uint pid)
        {
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", "somevalue"),
                    new XAttribute("unkattrii", "somevalueii"),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 11),
                    new XAttribute("oasis", (uint)type),
                    new XAttribute("pid", pid),
                    new XAttribute("time", GetCurrentTime())
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public string GetMissionCompletedMsg(uint pid, uint missionId)
        {
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", "somevalue"),
                    new XAttribute("unkattrii", "somevalueii"),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 1),
                    new XAttribute("oasis", (uint)type),
                    new XAttribute("pid", pid),
                    new XAttribute("time", GetCurrentTime()),
                    new XAttribute("missionid", missionId)
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public string GetAvatarChangedMsg(uint portraitId, uint bgColor)
        {
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", "somevalue"),
                    new XAttribute("unkattrii", "somevalueii"),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 11),
                    new XAttribute("oasis", (uint)type),
                    new XAttribute("pid", 0),//pid must be zero
                    new XAttribute("time", GetCurrentTime()),
                    new XAttribute("avatar", portraitId),
                    new XAttribute("bgcolor", bgColor)
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public void toBuffer(Stream s)
        {
            header.toBuffer(s);
            Helper.WriteString(s, m_body);
        }

        //The format is for SYSTEMTIME structure, read as follows: %u-%u-%u %u:%u:%f
        public string GetCurrentTime()
        {
            DateTime now = DateTime.UtcNow;
            return $"{now.Year}-{now.Month}-{now.Day} {now.Hour}:{now.Minute}:{(float)now.Second}";
        }
    }
}
