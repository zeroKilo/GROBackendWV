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

        public static List<List<string>> GetQueryResults(string query)
        {
            List<List<string>> result = new List<List<string>>();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                List<string> entry = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    entry.Add(reader[i].ToString());
                result.Add(entry);
            }
            reader.Close();
            reader.Dispose();
            command.Dispose();
            return result;
        }

        public static ClientInfo GetUserByName(string name)
        {
            ClientInfo result = null;
            List<List<string>> results = GetQueryResults("SELECT * FROM users WHERE name='" + name + "'");
            foreach(List<string> entry in results)
            {
                result = new ClientInfo();
                result.PID = Convert.ToUInt32(entry[1]);
                result.pass = entry[3];
                result.name = name;
            }
            return result;
        }

        public static GR5_Persona GetPersona(ClientInfo client)
        {
            List<List<string>> results = GetQueryResults("SELECT * FROM personas WHERE pid=" + client.PID);
            foreach (List<string> entry in results)
            {
                GR5_Persona p = new GR5_Persona();
                p.PersonaID = client.PID;
                p.Name = client.name;
                p.PortraitID = Convert.ToUInt32(entry[3]);
                p.DecoratorID = Convert.ToUInt32(entry[4]);
                p.AvatarBackgroundColor = Convert.ToUInt32(entry[5]);
                p.GRCash = Convert.ToUInt32(entry[6]);
                p.IGC = Convert.ToUInt32(entry[7]);
                p.AchievementPoints = Convert.ToUInt32(entry[8]);
                p.LastUsedCharacterID = Convert.ToByte(entry[9]);
                p.MaxInventorySlot = Convert.ToUInt32(entry[10]);
                p.MaxScrapYardSlot = Convert.ToUInt32(entry[11]);
                p.GhostRank = Convert.ToUInt32(entry[12]);
                p.Flag = Convert.ToUInt32(entry[13]);
                return p;
            }
            return null;
        }

        public static List<GR5_Character> GetCharacters(uint pid)
        {
            List<GR5_Character> result = new List<GR5_Character>();
            List<List<string>> results = GetQueryResults("SELECT * FROM characters WHERE pid=" + pid);
            foreach (List<string> entry in results)
            {
                GR5_Character c = new GR5_Character();
                c.PersonaID = pid;
                c.ClassID = Convert.ToUInt32(entry[2]);
                c.PEC = Convert.ToUInt32(entry[3]);
                c.Level = Convert.ToUInt32(entry[4]);
                c.UpgradePoints = Convert.ToUInt32(entry[5]);
                c.CurrentLevelPEC = Convert.ToUInt32(entry[6]);
                c.NextLevelPEC = Convert.ToUInt32(entry[7]);
                c.FaceID = Convert.ToByte(entry[8]);
                c.SkinToneID = Convert.ToByte(entry[9]);
                c.LoadoutKitID = Convert.ToByte(entry[10]);
                result.Add(c);
            }
            return result;
        }

        public static List<GR5_NewsMessage> GetNews(uint pid)
        {
            List<GR5_NewsMessage> result = new List<GR5_NewsMessage>();
            List<List<string>> results = GetQueryResults("SELECT * FROM news");
            foreach (List<string> entry in results)
            {
                GR5_NewsMessage message = new GR5_NewsMessage();
                message.header = new GR5_NewsHeader();
                message.header.m_ID = Convert.ToUInt32(entry[1]);
                message.header.m_recipientID = pid;
                message.header.m_recipientType = Convert.ToUInt32(entry[2]);
                message.header.m_publisherPID = Convert.ToUInt32(entry[3]);
                message.header.m_publisherName = entry[4];
                message.header.m_displayTime = (ulong)DateTime.UtcNow.Ticks;
                message.header.m_publicationTime = (ulong)DateTime.UtcNow.Ticks;
                message.header.m_expirationTime = (ulong)DateTime.UtcNow.AddDays(5).Ticks;
                message.header.m_title = entry[5];
                message.header.m_link = entry[6];
                message.m_body = entry[7];
                result.Add(message);
            }
            return result;
        }

        public static List<GR5_TemplateItem> GetTemplateItems()
        {
            List<GR5_TemplateItem> result = new List<GR5_TemplateItem>();
            List<List<string>> results = GetQueryResults("SELECT * FROM templateitems");
            foreach (List<string> entry in results)
            {
                GR5_TemplateItem item = new GR5_TemplateItem();
                item.m_ItemID = Convert.ToUInt32(entry[1]);
                item.m_ItemType = Convert.ToByte(entry[2]);
                item.m_ItemName = entry[3];
                item.m_DurabilityType = Convert.ToByte(entry[4]);
                item.m_IsInInventory = entry[5] == "True";
                item.m_IsSellable = entry[6] == "True";
                item.m_IsLootable = entry[7] == "True";
                item.m_IsRewardable = entry[8] == "True";
                item.m_IsUnlockable = entry[9] == "True";
                item.m_MaxItemInSlot = Convert.ToUInt32(entry[10]);
                item.m_GearScore = Convert.ToUInt32(entry[11]);
                item.m_IGCValue = Convert.ToUInt32(entry[12]) / 100f;
                item.m_OasisName = Convert.ToUInt32(entry[13]);
                item.m_OasisDesc = Convert.ToUInt32(entry[14]);
                result.Add(item);
            }
            return result;
        }

        public static List<GR5_InventoryBag> GetInventoryBags(uint pid, byte type)
        {
            List<GR5_InventoryBag> result = new List<GR5_InventoryBag>();
            List<int> bagIDs = new List<int>();
            List<List<string>> results = GetQueryResults("SELECT * FROM inventorybags WHERE pid=" + pid + " AND bagtype =" + type);
            foreach (List<string> entry in results)
                bagIDs.Add(Convert.ToInt32(entry[0]));
            foreach(int bagID in bagIDs)
            {
                GR5_InventoryBag bag = new GR5_InventoryBag();
                bag.m_PersonaID = pid;
                bag.m_InventoryBagType = type;
                bag.m_InventoryBagSlotVector = new List<GR5_InventoryBagSlot>();
                results = GetQueryResults("SELECT * FROM inventorybagslots WHERE bagid=" + bagID);
                foreach (List<string> entry in results)
                {
                    GR5_InventoryBagSlot slot = new GR5_InventoryBagSlot();
                    slot.InventoryID = Convert.ToUInt32(entry[2]);
                    slot.SlotID = Convert.ToUInt32(entry[3]);
                    slot.Durability = Convert.ToUInt32(entry[4]);
                    bag.m_InventoryBagSlotVector.Add(slot);
                }
                result.Add(bag);
            }
            return result;
        }

        public static List<GR5_UserItem> GetUserItems(uint pid, byte type)
        {
            List<GR5_UserItem> result = new List<GR5_UserItem>();
            List<List<string>> results = GetQueryResults("SELECT * FROM useritems WHERE pid=" + pid + " AND itemtype =" + type);
            foreach (List<string> entry in results)
            {
                GR5_UserItem item = new GR5_UserItem();
                item.InventoryID = Convert.ToUInt32(entry[1]);
                item.PersonaID = pid;
                item.ItemType = type;
                item.ItemID = Convert.ToUInt32(entry[4]);
                item.OasisName = Convert.ToUInt32(entry[5]);
                item.IGCPrice = Convert.ToUInt32(entry[6]);
                item.GRCashPrice = Convert.ToUInt32(entry[7]);
                result.Add(item);
            }
            return result;
        }

        public static List<GR5_Ability> GetAbilities()
        {
            List<GR5_Ability> result = new List<GR5_Ability>();
            List<List<string>> results = GetQueryResults("SELECT * FROM abilities");
            foreach (List<string> entry in results)
            {
                GR5_Ability a = new GR5_Ability();
                a.Id = Convert.ToUInt32(entry[1]);
                a.SlotCount = Convert.ToByte(entry[2]);
                a.ClassID = Convert.ToByte(entry[3]);
                a.AbilityType = Convert.ToByte(entry[4]);
                a.ModifierListId = Convert.ToUInt32(entry[5]);
                result.Add(a);
            }
            return result;
        }

        public static List<GR5_LoadoutKit> GetLoadoutKits(uint pid)
        {
            List<GR5_LoadoutKit> result = new List<GR5_LoadoutKit>();
            List<List<string>> results = GetQueryResults("SELECT * FROM loadoutkits WHERE pid=" + pid);
            foreach (List<string> entry in results)
            {
                GR5_LoadoutKit kit = new GR5_LoadoutKit();
                kit.m_LoadoutKitID = Convert.ToUInt32(entry[2]);
                kit.m_ClassID = Convert.ToUInt32(entry[3]);
                kit.m_Weapon1ID = Convert.ToUInt32(entry[4]);
                kit.m_Weapon2ID = Convert.ToUInt32(entry[5]);
                kit.m_Weapon3ID = Convert.ToUInt32(entry[6]);
                kit.m_Item1ID = Convert.ToUInt32(entry[7]);
                kit.m_Item2ID = Convert.ToUInt32(entry[8]);
                kit.m_Item3ID = Convert.ToUInt32(entry[9]);
                kit.m_PowerID = Convert.ToUInt32(entry[10]);
                kit.m_HelmetID = Convert.ToUInt32(entry[11]);
                kit.m_ArmorID = Convert.ToUInt32(entry[12]);
                kit.m_OasisDesc = Convert.ToUInt32(entry[13]);
                kit.m_Flag = Convert.ToUInt32(entry[14]);
                result.Add(kit);
            }
            return result;
        }

        public static List<GR5_ArmorInsert> GetArmorInserts()
        {
            List<GR5_ArmorInsert> result = new List<GR5_ArmorInsert>();
            List<List<string>> results = GetQueryResults("SELECT * FROM armorinserts");
            foreach (List<string> entry in results)
            {
                GR5_ArmorInsert insert = new GR5_ArmorInsert();
                insert.Id = Convert.ToUInt32(entry[1]);
                insert.Type = Convert.ToByte(entry[2]);
                insert.AssetKey = Convert.ToUInt32(entry[3]);
                insert.ModifierListID = Convert.ToUInt32(entry[4]);
                insert.CharacterID = Convert.ToByte(entry[5]);
                result.Add(insert);
            }
            return result;
        }

        public static List<GR5_ArmorItem> GetArmorItems()
        {
            List<GR5_ArmorItem> result = new List<GR5_ArmorItem>();
            List<List<string>> results = GetQueryResults("SELECT * FROM armoritems");
            foreach (List<string> entry in results)
            {
                GR5_ArmorItem item = new GR5_ArmorItem();
                item.Id = Convert.ToUInt32(entry[1]);
                item.Type = Convert.ToByte(entry[2]);
                item.AssetKey = Convert.ToUInt32(entry[3]);
                item.ModifierListID = Convert.ToUInt32(entry[4]);
                item.CharacterID = Convert.ToByte(entry[5]);
                result.Add(item);
            }
            return result;
        }

        public static List<GR5_ArmorTier> GetArmorTiers()
        {
            List<GR5_ArmorTier> result = new List<GR5_ArmorTier>();
            List<List<string>> results = GetQueryResults("SELECT * FROM armortiers");
            foreach (List<string> entry in results)
            {
                GR5_ArmorTier tier = new GR5_ArmorTier();
                tier.Id = Convert.ToUInt32(entry[1]);
                tier.Type = Convert.ToByte(entry[2]);
                tier.Tier = Convert.ToByte(entry[3]);
                tier.ClassID = Convert.ToByte(entry[4]);
                tier.UnlockLevel = Convert.ToByte(entry[5]);
                tier.InsertSlots = Convert.ToByte(entry[6]);
                tier.AssetKey = Convert.ToUInt32(entry[7]);
                tier.ModifierListId = Convert.ToUInt32(entry[8]);
                result.Add(tier);
            }
            return result;
        }

        public static List<GR5_PersonaArmorTier> GetPersonaArmorTiers(uint pid, uint tier)
        {
            List<GR5_PersonaArmorTier> result = new List<GR5_PersonaArmorTier>();
            List<int> IDs = new List<int>();
            List<uint> tierIDs = new List<uint>();
            List<List<string>> results = GetQueryResults("SELECT * FROM personaarmortiers WHERE tierid=" + tier);
            foreach (List<string> entry in results)
            {
                IDs.Add(Convert.ToInt32(entry[0]));
                tierIDs.Add(Convert.ToUInt32(entry[1]));
            }
            for(int i = 0; i < IDs.Count;i++)
            {
                GR5_PersonaArmorTier pat = new GR5_PersonaArmorTier();
                pat.ArmorTierID = tierIDs[i];
                pat.Inserts = new List<GR5_ArmorInsertSlot>();
                results = GetQueryResults("SELECT * FROM armorinsertslots WHERE patid=" + IDs[i]);
                foreach (List<string> entry in results)
                {
                    GR5_ArmorInsertSlot slot = new GR5_ArmorInsertSlot();
                    slot.InsertID = Convert.ToUInt32(entry[2]);
                    slot.Durability = Convert.ToUInt32(entry[3]);
                    slot.SlotID = Convert.ToByte(entry[4]);
                    pat.Inserts.Add(slot);
                }
                result.Add(pat);
            }
            return result;
        }

        public static List<GR5_SkillModifier> GetSkillModefiers()
        {
            List<GR5_SkillModifier> result = new List<GR5_SkillModifier>();
            List<List<string>> results = GetQueryResults("SELECT * FROM skillmodifiers");
            foreach (List<string> entry in results)
            {
                GR5_SkillModifier mod = new GR5_SkillModifier();
                mod.m_ModifierID = Convert.ToUInt32(entry[1]);
                mod.m_ModifierType = Convert.ToByte(entry[2]);
                mod.m_PropertyType = Convert.ToByte(entry[3]);
                mod.m_MethodType = Convert.ToByte(entry[4]);
                mod.m_MethodValue = entry[5];
                result.Add(mod);
            }
            return result;
        }

        public static List<GR5_SkillModifierList> GetSkillModefierLists()
        {
            List<GR5_SkillModifierList> result = new List<GR5_SkillModifierList>();
            List<uint> IDs = new List<uint>();
            List<List<string>> results = GetQueryResults("SELECT * FROM skillmodifierlists");
            foreach (List<string> entry in results)
                IDs.Add(Convert.ToUInt32(entry[1]));
            foreach (uint id in IDs)
            {
                GR5_SkillModifierList list = new GR5_SkillModifierList();
                list.m_ID = id;
                list.m_ModifierIDVector = new List<uint>();
                results = GetQueryResults("SELECT * FROM skillmodifierlistentries WHERE listid=" + id);
                foreach (List<string> entry in results)
                    list.m_ModifierIDVector.Add(Convert.ToUInt32(entry[2]));
                result.Add(list);
            }
            return result;
        }

        public static List<GR5_FaceSkinTone> GetFaceSkinTones()
        {
            List<GR5_FaceSkinTone> result = new List<GR5_FaceSkinTone>();
            List<List<string>> results = GetQueryResults("SELECT * FROM faceskintones");
            foreach (List<string> entry in results)
            {
                GR5_FaceSkinTone mod = new GR5_FaceSkinTone();
                mod.id = Convert.ToUInt32(entry[1]);
                mod.objectType= Convert.ToByte(entry[2]);
                mod.objectKey = Convert.ToUInt32(entry[3]);
                mod.oasisName = Convert.ToUInt32(entry[4]);
                result.Add(mod);
            }
            return result;
        }

        public static List<Map_U32_GR5_Weapon> GetTemplateWeaponList()
        {
            List<Map_U32_GR5_Weapon> result = new List<Map_U32_GR5_Weapon>();
            List<List<string>> results = GetQueryResults("SELECT * FROM weapons");
            foreach (List<string> entry in results)
            {
                Map_U32_GR5_Weapon pair = new Map_U32_GR5_Weapon();
                pair.key = Convert.ToUInt32(entry[1]);
                pair.weapon = new GR5_Weapon();
                pair.weapon.weaponID = Convert.ToUInt32(entry[2]);
                pair.weapon.classTypeID = Convert.ToUInt32(entry[3]);
                pair.weapon.weaponType = Convert.ToUInt32(entry[4]);
                pair.weapon.equippableClassTypeID = Convert.ToUInt32(entry[5]);
                pair.weapon.flags = Convert.ToUInt32(entry[6]);
                result.Add(pair);
            }
            return result;
        }

        public static List<Map_U32_GR5_Component> GetComponents()
        {
            List<Map_U32_GR5_Component> result = new List<Map_U32_GR5_Component>();
            List<List<string>> results = GetQueryResults("SELECT * FROM components");
            foreach (List<string> entry in results)
            {
                Map_U32_GR5_Component pair = new Map_U32_GR5_Component();
                pair.key = Convert.ToUInt32(entry[1]);
                pair.component = new GR5_Component();
                pair.component.componentID = Convert.ToUInt32(entry[2]);
                pair.component.componentKey = Convert.ToUInt32(entry[3]);
                pair.component.componentType = Convert.ToByte(entry[4]);
                pair.component.boneStructure = Convert.ToUInt32(entry[5]);
                pair.component.modifierListID = Convert.ToUInt32(entry[6]);
                result.Add(pair);
            }
            return result;
        }

        public static List<Map_U32_VectorU32> GetTemplateComponentLists()
        {
            List<Map_U32_VectorU32> result = new List<Map_U32_VectorU32>();
            List<List<string>> results = GetQueryResults("SELECT * FROM tempcomponentlists");
            foreach (List<string> entry in results)
            {
                GR5_Component c = new GR5_Component();
                uint key = Convert.ToUInt32(entry[1]);
                uint value = Convert.ToUInt32(entry[2]);
                bool found = false;
                foreach (Map_U32_VectorU32 pair in result)
                    if (pair.key == key)
                    {
                        found = true;
                        pair.vector.Add(value);
                        break;
                    }
                if (!found)
                {
                    Map_U32_VectorU32 pair = new Map_U32_VectorU32();
                    pair.key = key;
                    pair.vector.Add(value);
                    result.Add(pair);
                }
            }
            return result;
        }

        public static List<Map_U32_VectorU32> GetWeaponCompatibilityBridge()
        {
            List<Map_U32_VectorU32> result = new List<Map_U32_VectorU32>();
            List<List<string>> results = GetQueryResults("SELECT * FROM weaponcompatbridge");
            foreach (List<string> entry in results)
            {
                GR5_Component c = new GR5_Component();
                uint key = Convert.ToUInt32(entry[1]);
                uint value = Convert.ToUInt32(entry[2]);
                bool found = false;
                foreach (Map_U32_VectorU32 pair in result)
                    if (pair.key == key)
                    {
                        found = true;
                        pair.vector.Add(value);
                        break;
                    }
                if (!found)
                {
                    Map_U32_VectorU32 pair = new Map_U32_VectorU32();
                    pair.key = key;
                    pair.vector.Add(value);
                    result.Add(pair);
                }
            }
            return result;
        }
    }
}
