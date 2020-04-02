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
            Log.WriteLine(1, "DB loaded...");
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
                result.Add(message);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_TemplateItem> GetTemplateItems()
        {
            List<GR5_TemplateItem> result = new List<GR5_TemplateItem>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM templateitems";
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_TemplateItem item = new GR5_TemplateItem();
                item.m_ItemID = Convert.ToUInt32(reader[0].ToString());
                item.m_ItemType = Convert.ToByte(reader[1].ToString());
                item.m_ItemName = reader[2].ToString();
                item.m_DurabilityType = Convert.ToByte(reader[3].ToString());
                item.m_IsInInventory = reader[4].ToString() == "True";
                item.m_IsSellable = reader[5].ToString() == "True";
                item.m_IsLootable = reader[6].ToString() == "True";
                item.m_IsRewardable = reader[7].ToString() == "True";
                item.m_IsUnlockable = reader[8].ToString() == "True";
                item.m_MaxItemInSlot = Convert.ToUInt32(reader[9].ToString());
                item.m_GearScore = Convert.ToUInt32(reader[10].ToString());
                item.m_IGCValue = Convert.ToUInt32(reader[11].ToString()) / 100f;
                item.m_OasisName = Convert.ToUInt32(reader[12].ToString());
                item.m_OasisDesc = Convert.ToUInt32(reader[13].ToString());
                result.Add(item);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_InventoryBag> GetInventoryBags(uint pid, byte type)
        {
            List<GR5_InventoryBag> result = new List<GR5_InventoryBag>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM inventorybags WHERE pid=" + pid + " AND bagtype =" + type;
            SQLiteDataReader reader = command.ExecuteReader();
            List<int> bagIDs = new List<int>();
            while (reader.Read())
                bagIDs.Add(Convert.ToInt32(reader[0].ToString()));
            reader.Close();
            reader.Dispose();
            command.Dispose();
            foreach (int bagID in bagIDs)
            {
                GR5_InventoryBag bag = new GR5_InventoryBag();
                bag.m_PersonaID = pid;
                bag.m_InventoryBagType = type;
                command = new SQLiteCommand(connection);
                command.CommandText = "SELECT * FROM inventorybagslots WHERE bagid=" + bagID;
                reader = command.ExecuteReader();
                bag.m_InventoryBagSlotVector = new List<GR5_InventoryBagSlot>();
                while (reader.Read())
                {
                    GR5_InventoryBagSlot slot = new GR5_InventoryBagSlot();
                    slot.InventoryID = Convert.ToUInt32(reader[2].ToString());
                    slot.SlotID = Convert.ToUInt32(reader[3].ToString());
                    slot.Durability = Convert.ToUInt32(reader[4].ToString());
                    bag.m_InventoryBagSlotVector.Add(slot);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                result.Add(bag);
            }
            return result;
        }

        public static List<GR5_UserItem> GetUserItems(uint pid, byte type)
        {
            List<GR5_UserItem> result = new List<GR5_UserItem>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM useritems WHERE pid=" + pid + " AND itemtype =" + type;
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_UserItem item = new GR5_UserItem();
                item.InventoryID = Convert.ToUInt32(reader[1].ToString());
                item.PersonaID = pid;
                item.ItemType = type;
                item.ItemID = Convert.ToUInt32(reader[4].ToString());
                item.OasisName = Convert.ToUInt32(reader[5].ToString());
                item.IGCPrice = Convert.ToUInt32(reader[6].ToString());
                item.GRCashPrice = Convert.ToUInt32(reader[7].ToString());
                result.Add(item);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_Ability> GetAbilities()
        {
            List<GR5_Ability> result = new List<GR5_Ability>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM abilities";
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_Ability a = new GR5_Ability();
                a.Id = Convert.ToUInt32(reader[1].ToString());
                a.SlotCount = Convert.ToByte(reader[2].ToString());
                a.ClassID = Convert.ToByte(reader[3].ToString());
                a.AbilityType = Convert.ToByte(reader[4].ToString());
                a.ModifierListId = Convert.ToUInt32(reader[5].ToString());
                result.Add(a);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_LoadoutKit> GetLoadoutKits(uint pid)
        {
            List<GR5_LoadoutKit> result = new List<GR5_LoadoutKit>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM loadoutkits WHERE pid=" + pid;
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_LoadoutKit kit = new GR5_LoadoutKit();
                kit.m_LoadoutKitID = Convert.ToUInt32(reader[1].ToString());
                kit.m_ClassID = Convert.ToUInt32(reader[2].ToString());
                kit.m_Weapon1ID = Convert.ToUInt32(reader[3].ToString());
                kit.m_Weapon2ID = Convert.ToUInt32(reader[4].ToString());
                kit.m_Weapon3ID = Convert.ToUInt32(reader[5].ToString());
                kit.m_Item1ID = Convert.ToUInt32(reader[6].ToString());
                kit.m_Item2ID = Convert.ToUInt32(reader[7].ToString());
                kit.m_Item3ID = Convert.ToUInt32(reader[8].ToString());
                kit.m_PowerID = Convert.ToUInt32(reader[9].ToString());
                kit.m_HelmetID = Convert.ToUInt32(reader[10].ToString());
                kit.m_ArmorID = Convert.ToUInt32(reader[11].ToString());
                kit.m_OasisDesc = Convert.ToUInt32(reader[12].ToString());
                kit.m_Flag = Convert.ToUInt32(reader[13].ToString());
                result.Add(kit);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_ArmorInsert> GetArmorInserts()
        {
            List<GR5_ArmorInsert> result = new List<GR5_ArmorInsert>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM armorinserts";
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_ArmorInsert insert = new GR5_ArmorInsert();
                insert.Id = Convert.ToUInt32(reader[1].ToString());
                insert.Type = Convert.ToByte(reader[2].ToString());
                insert.AssetKey = Convert.ToUInt32(reader[3].ToString());
                insert.ModifierListID = Convert.ToUInt32(reader[4].ToString());
                insert.CharacterID = Convert.ToByte(reader[5].ToString());
                result.Add(insert);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_ArmorItem> GetArmorItems()
        {
            List<GR5_ArmorItem> result = new List<GR5_ArmorItem>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM armoritems";
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_ArmorItem item = new GR5_ArmorItem();
                item.Id = Convert.ToUInt32(reader[1].ToString());
                item.Type = Convert.ToByte(reader[2].ToString());
                item.AssetKey = Convert.ToUInt32(reader[3].ToString());
                item.ModifierListID = Convert.ToUInt32(reader[4].ToString());
                item.CharacterID = Convert.ToByte(reader[5].ToString());
                result.Add(item);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_ArmorTier> GetArmorTiers()
        {
            List<GR5_ArmorTier> result = new List<GR5_ArmorTier>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM armortiers";
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_ArmorTier tier = new GR5_ArmorTier();
                tier.Id = Convert.ToUInt32(reader[1].ToString());
                tier.Type = Convert.ToByte(reader[2].ToString());
                tier.Tier = Convert.ToByte(reader[3].ToString());
                tier.ClassID = Convert.ToByte(reader[4].ToString());
                tier.UnlockLevel = Convert.ToByte(reader[5].ToString());
                tier.InsertSlots = Convert.ToByte(reader[6].ToString());
                tier.AssetKey = Convert.ToUInt32(reader[7].ToString());
                tier.ModifierListId = Convert.ToUInt32(reader[8].ToString());
                result.Add(tier);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_PersonaArmorTier> GetPersonaArmorTiers(uint pid, uint tier)
        {
            List<GR5_PersonaArmorTier> result = new List<GR5_PersonaArmorTier>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM personaarmortiers WHERE tierid=" + tier;
            SQLiteDataReader reader = command.ExecuteReader();
            List<int> IDs = new List<int>();
            List<uint> tierIDs = new List<uint>();
            while (reader.Read())
            {
                IDs.Add(Convert.ToInt32(reader[0].ToString()));
                tierIDs.Add(Convert.ToUInt32(reader[1].ToString()));
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            for(int i = 0; i < IDs.Count;i++)
            {
                GR5_PersonaArmorTier pat = new GR5_PersonaArmorTier();
                pat.ArmorTierID = tierIDs[i];
                command = new SQLiteCommand(connection);
                command.CommandText = "SELECT * FROM armorinsertslots WHERE patid=" + IDs[i];
                reader = command.ExecuteReader();
                pat.Inserts = new List<GR5_ArmorInsertSlot>();
                while (reader.Read())
                {
                    GR5_ArmorInsertSlot slot = new GR5_ArmorInsertSlot();
                    slot.InsertID = Convert.ToUInt32(reader[2].ToString());
                    slot.Durability = Convert.ToUInt32(reader[3].ToString());
                    slot.SlotID = Convert.ToByte(reader[4].ToString());
                    pat.Inserts.Add(slot);
                }
                reader.Close();
                reader.Dispose();
                command.Dispose();
                result.Add(pat);
            }
            return result;
        }

        public static List<GR5_SkillModifier> GetSkillModefiers()
        {
            List<GR5_SkillModifier> result = new List<GR5_SkillModifier>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM skillmodifiers";
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                GR5_SkillModifier mod = new GR5_SkillModifier();
                mod.m_ModifierID = Convert.ToUInt32(reader[1].ToString());
                mod.m_ModifierType = Convert.ToByte(reader[2].ToString());
                mod.m_PropertyType = Convert.ToByte(reader[3].ToString());
                mod.m_MethodType = Convert.ToByte(reader[4].ToString());
                mod.m_MethodValue = reader[5].ToString();
                result.Add(mod);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static List<GR5_SkillModifierList> GetSkillModefierLists()
        {
            List<GR5_SkillModifierList> result = new List<GR5_SkillModifierList>();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM skillmodifierlists";
            SQLiteDataReader reader = command.ExecuteReader();
            List<uint> IDs = new List<uint>();
            while (reader.Read())
                IDs.Add(Convert.ToUInt32(reader[1].ToString()));
            reader.Close();
            reader.Dispose();
            command.Dispose();
            foreach (uint id in IDs)
            {
                GR5_SkillModifierList list = new GR5_SkillModifierList();
                list.m_ID = id;
                list.m_ModifierIDVector = new List<uint>();
                command = new SQLiteCommand(connection);
                command.CommandText = "SELECT * FROM skillmodifierlistentries WHERE listid=" + id;
                reader = command.ExecuteReader();
                while (reader.Read())
                    list.m_ModifierIDVector.Add(Convert.ToUInt32(reader[2].ToString()));
                reader.Close();
                reader.Dispose();
                command.Dispose();
                result.Add(list);
            }
            return result;
        }
    }
}
