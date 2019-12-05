using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace GRPBackendWV
{
    public static class DBHelper
    {
        public static SQLiteConnection connection = new SQLiteConnection();

        public static void Init()
        {
            connection.ConnectionString = "Data Source=database.sqlite";
            connection.Open();
            Log.WriteLine("DB loaded...");
        }

        public static ClientInfo GetUserByName(string name)
        {
            ClientInfo result = null;
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM users WHERE name='" + name + "'";
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                result = new ClientInfo();
                result.PID = Convert.ToUInt32(reader[1].ToString());
                result.pass = reader[3].ToString();
                result.name = name;
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_Character> GetUserCharacters(uint pid)
        {
            List<GR5_Character> result = new List<GR5_Character>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM characters WHERE pid=" + pid;
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_Character c = new GR5_Character();
                c.PersonaID = pid;
                c.ClassID = Convert.ToUInt32(reader[2].ToString());
                c.PEC = Convert.ToUInt32(reader[3].ToString());
                c.Level = Convert.ToUInt32(reader[4].ToString());
                c.UpgradePoints = Convert.ToUInt32(reader[5].ToString());
                c.CurrentLevelPEC = Convert.ToUInt32(reader[6].ToString());
                c.NextLevelPEC = Convert.ToUInt32(reader[7].ToString());
                c.FaceID = Convert.ToByte(reader[8].ToString());
                c.SkinToneID = Convert.ToByte(reader[9].ToString());
                c.LoadoutKitID = Convert.ToByte(reader[10].ToString());
                result.Add(c);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_NewsMessage> GetNews(uint pid)
        {
            List<GR5_NewsMessage> result = new List<GR5_NewsMessage>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM news";
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_NewsMessage message = new GR5_NewsMessage();
                message.header = new GR5_NewsHeader();
                message.header.m_ID = Convert.ToUInt32(reader[0].ToString());
                message.header.m_recipientID = pid;
                message.header.m_recipientType = Convert.ToUInt32(reader[1].ToString());
                message.header.m_publisherPID = Convert.ToUInt32(reader[2].ToString());
                message.header.m_publisherName = reader[3].ToString();
                message.header.m_displayTime = (ulong)DateTime.UtcNow.Ticks;
                message.header.m_publicationTime = (ulong)DateTime.UtcNow.Ticks;
                message.header.m_expirationTime = (ulong)DateTime.UtcNow.AddDays(5).Ticks;
                message.header.m_title = reader[4].ToString();
                message.header.m_link = reader[5].ToString();
                message.m_body = reader[6].ToString();
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_TemplateItem> GetTemplateItems()
        {
            List<GR5_TemplateItem> result = new List<GR5_TemplateItem>();
            return result;
        }
    }
}
