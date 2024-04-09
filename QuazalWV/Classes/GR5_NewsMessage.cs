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
        WeaponKills = 73504,
        WeaponHeadshots = 73505,
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
                    m_body = GetAvatarChangedMsg(publisher.PID, arg1, arg2);
                    break;
                case NewsMessageType.LevelUp:
                    m_body = GetLevelUpMsg(publisher.PID, arg1, arg2);
                    break;
                case NewsMessageType.AchievementCompleted:
                    m_body = GetAchievementCompletedMsg(publisher.PID, arg1);
                    break;
                case NewsMessageType.MissionCompleted:
                    m_body = GetMissionCompletedMsg(publisher.PID, arg1);
                    break;
                case NewsMessageType.WeaponLevelUp:
                    m_body = GetWeaponLevelUpMsg(publisher.PID, arg1, arg2);
                    break;
                case NewsMessageType.WeaponKills:
                    m_body = GetWeaponKillsMsg(publisher.PID, arg1, arg2);
                    break;
                case NewsMessageType.WeaponHeadshots:
                    m_body = GetWeaponKillsMsg(publisher.PID, arg1, arg2);
                    break;
                case NewsMessageType.RewardReceived:
                    m_body = GetRewardReceivedMsg(publisher.PID, arg1);
                    break;
            }
        }

        public string GetWelcomeMsg(uint pid)
        {
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", ""),
                    new XAttribute("unkattrii", ""),
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
                    new XAttribute("unkattr", ""),
                    new XAttribute("unkattrii", ""),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 1),
                    new XAttribute("oasis", (uint)type),
                    new XAttribute("pid", pid),
                    new XAttribute("time", GetCurrentTime()),
                    new XAttribute("missionid", missionId)
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public string GetAvatarChangedMsg(uint pid, uint portraitId, uint bgColor)
        {
            pid = 4661;
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", ""),
                    new XAttribute("unkattrii", ""),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 11),
                    new XAttribute("oasis", (uint)type),
                    // a friend's PID
                    new XAttribute("pid", pid),
                    new XAttribute("time", GetCurrentTime()),
                    // the new avatar
                    new XAttribute("avatar", portraitId),
                    new XAttribute("bgcolor", bgColor)
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public string GetLevelUpMsg(uint pid, uint classId, uint level)
        {
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", ""),
                    new XAttribute("unkattrii", ""),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 11),
                    new XAttribute("oasis", (uint)type),
                    new XAttribute("pid", pid),
                    new XAttribute("time", GetCurrentTime()),
                    new XAttribute("class", classId),
                    new XAttribute("level", level)
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public string GetAchievementCompletedMsg(uint pid, uint achievementId)
        {
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", ""),
                    new XAttribute("unkattrii", ""),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 1),
                    new XAttribute("oasis", (uint)type),
                    new XAttribute("pid", pid),
                    new XAttribute("time", GetCurrentTime()),
                    new XAttribute("achievementid", achievementId)
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public string GetWeaponLevelUpMsg(uint pid, uint weaponId, uint level)
        {
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", ""),
                    new XAttribute("unkattrii", ""),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 1),
                    new XAttribute("oasis", (uint)type),
                    new XAttribute("pid", pid),
                    new XAttribute("time", GetCurrentTime()),
                    new XAttribute("weaponid", weaponId),
                    new XAttribute("level", level)
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public string GetWeaponKillsMsg(uint pid, uint weaponId, uint kills)
        {
            pid = 4661;
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", ""),
                    new XAttribute("unkattrii", ""),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 1),
                    new XAttribute("oasis", (uint)type),
                    // a friend's PID
                    new XAttribute("pid", pid),
                    new XAttribute("time", GetCurrentTime()),
                    new XAttribute("weaponid", weaponId),
                    new XAttribute("kills", kills)
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public string GetRewardReceivedMsg(uint pid, uint accoladeId)
        {
            pid = 4661;
            return
            new XElement("news",
                new XElement("message",
                    new XAttribute("unkattr", ""),
                    new XAttribute("unkattrii", ""),
                    new XAttribute("type", (uint)type),
                    new XAttribute("icon", 1),
                    new XAttribute("oasis", (uint)type),
                    // a friend's PID
                    new XAttribute("pid", pid),
                    new XAttribute("time", GetCurrentTime()),
                    new XAttribute("accoladeid", accoladeId)
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
        }

        public void toBuffer(Stream s)
        {
            header.toBuffer(s);
            Helper.WriteString(s, m_body);
        }

        /// <summary>
        /// The format is for SYSTEMTIME structure, read as follows: %u-%u-%u %u:%u:%f
        /// </summary>
        /// <returns></returns>
        public string GetCurrentTime()
        {
            DateTime now = DateTime.UtcNow;
            return $"{now.Year}-{now.Month}-{now.Day} {now.Hour}:{now.Minute}:{(float)now.Second}";
        }
    }
}
