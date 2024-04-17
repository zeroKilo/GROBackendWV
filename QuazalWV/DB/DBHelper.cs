using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace QuazalWV
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

        /// <summary>
        /// Gets news message headers and bodies, deprecated
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static List<GR5_NewsMessage> GetNews(uint pid, string body)
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
                //m_body is in XML format, for now i hardcoded it in GR5_NewsMessage.cs
                message.m_body = body;
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
                GR5_TemplateItem item = new GR5_TemplateItem
                {
                    m_ItemID = Convert.ToUInt32(entry[1]),
                    m_ItemType = Convert.ToByte(entry[2]),
                    m_ItemName = entry[3],
                    m_DurabilityType = Convert.ToByte(entry[4]),
                    m_IsInInventory = entry[5] == "True",
                    m_IsSellable = entry[6] == "True",
                    m_IsLootable = entry[7] == "True",
                    m_IsRewardable = entry[8] == "True",
                    m_IsUnlockable = entry[9] == "True",
                    m_MaxItemInSlot = Convert.ToUInt32(entry[10]),
                    m_GearScore = Convert.ToUInt32(entry[11]),
                    m_IGCValue = Convert.ToUInt32(entry[12]) / 100f,
                    m_OasisName = Convert.ToUInt32(entry[13]),
                    m_OasisDesc = Convert.ToUInt32(entry[14])
                };
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

        public static List<GR5_SkillModifier> GetSkillModifiers()
        {
            List<GR5_SkillModifier> result = new List<GR5_SkillModifier>();
            List<List<string>> results = GetQueryResults("SELECT * FROM skillmodifiers");
            foreach (List<string> entry in results)
            {
                GR5_SkillModifier mod = new GR5_SkillModifier
                {
                    m_ModifierID = Convert.ToUInt32(entry[2]),
                    m_ModifierType = Convert.ToByte(entry[3]),
                    m_PropertyType = Convert.ToByte(entry[4]),
                    m_MethodType = Convert.ToByte(entry[5]),
                    m_MethodValue = entry[6]
                };
                result.Add(mod);
            }
            return result;
        }

        public static List<GR5_SkillModifierList> GetSkillModifierLists()
        {
            List<GR5_SkillModifierList> result = new List<GR5_SkillModifierList>();
            List<List<string>> results = GetQueryResults("SELECT * FROM skillmodifiers");
            foreach (List<string> entry in results)
            {
                bool found = false;
                GR5_SkillModifierList target = null;
                foreach(GR5_SkillModifierList list in result)
                    if (list.m_ID == Convert.ToUInt32(entry[1]))
                    {
                        found = true;
                        target = list;
                        break;
                    }
                if (!found)
                {
                    target = new GR5_SkillModifierList();
                    target.m_ID = Convert.ToUInt32(entry[1]);
                    result.Add(target);
                }
                target.m_ModifierIDVector.Add(Convert.ToUInt32(entry[2]));
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
                Map_U32_GR5_Weapon pair = new Map_U32_GR5_Weapon
                {
                    key = Convert.ToUInt32(entry[1]),
                    weapon = new GR5_Weapon
                    {
                        weaponID = Convert.ToUInt32(entry[2]),
                        classTypeID = Convert.ToUInt32(entry[3]),
                        weaponType = Convert.ToUInt32(entry[4]),
                        equippableClassTypeID = Convert.ToUInt32(entry[5]),
                        flags = Convert.ToUInt32(entry[6])
                    }
                };
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

        public static List<GR5_AMM_Playlist> GetAMMPlaylists()
        {
            List<GR5_AMM_Playlist> result = new List<GR5_AMM_Playlist>();
            List<List<string>> results = GetQueryResults("SELECT * FROM amm_playlists");
            foreach (List<string> entry in results)
            {
                GR5_AMM_Playlist pl = new GR5_AMM_Playlist
                {
                    uiId = Convert.ToUInt32(entry[1]),
                    uiNodeType = Convert.ToUInt32(entry[2]),
                    uiMaxTeamSize = Convert.ToUInt32(entry[3]),
                    uiMinTeamSize = Convert.ToUInt32(entry[4]),
                    uiOasisNameId = Convert.ToUInt32(entry[5]),
                    uiOasisDescriptionId = Convert.ToUInt32(entry[6]),
                    uiIsRepeatable = Convert.ToUInt32(entry[7]),
                    uiIsRandom = Convert.ToUInt32(entry[8]),
                    uiThumbnailId = Convert.ToUInt32(entry[9]),
                    m_PlaylistEntryVector = new List<GR5_AMM_PlaylistEntry>()
                };
                List<List<string>> results2 = GetQueryResults("SELECT * FROM amm_playlistentries WHERE listID=" + pl.uiId);
                foreach (List<string> entry2 in results2)
                {
                    GR5_AMM_PlaylistEntry ple = new GR5_AMM_PlaylistEntry
                    {
                        uiMapId = Convert.ToUInt32(entry2[2]),
                        uiGameMode = Convert.ToUInt32(entry2[3]),
                        uiMatchDetail = Convert.ToUInt32(entry2[4])
                    };
                    pl.m_PlaylistEntryVector.Add(ple);
                }
                result.Add(pl);
            }
            return result;
        }

        public static List<GR5_AMM_Map> GetAMMMaps()
        {
            List<GR5_AMM_Map> result = new List<GR5_AMM_Map>();
            List<List<string>> results = GetQueryResults("SELECT * FROM amm_maps");
            foreach (List<string> entry in results)
            {
                GR5_AMM_Map map = new GR5_AMM_Map
                {
                    uiId = Convert.ToUInt32(entry[1]),
                    uiRootModifierId = Convert.ToUInt32(entry[2]),
                    uiMapKey = Convert.ToUInt32(entry[3]),
                    uiOasisNameId = Convert.ToUInt32(entry[4]),
                    uiOasisDescriptionId = Convert.ToUInt32(entry[5]),
                    uiThumbnailId = Convert.ToUInt32(entry[6]),
                    m_ModifierVector = new List<GR5_AMM_Modifier>()
                };
                List<List<string>> results2 = GetQueryResults("SELECT * FROM amm_modifiers WHERE listType=0 AND listID=" + map.uiId);
                foreach (List<string> entry2 in results2)
                {
                    GR5_AMM_Modifier mm = new GR5_AMM_Modifier
                    {
                        uiId = Convert.ToUInt32(entry2[3]),
                        uiParentId = Convert.ToUInt32(entry2[4]),
                        uiType = Convert.ToUInt32(entry2[5]),
                        uiValue = entry2[6]
                    };
                    map.m_ModifierVector.Add(mm);
                }
                result.Add(map);
            }
            return result;
        }

        public static List<GR5_AMM_GameMode> GetAMMGameModes()
        {
            List<GR5_AMM_GameMode> result = new List<GR5_AMM_GameMode>();
            List<List<string>> results = GetQueryResults("SELECT * FROM amm_gamemodes");
            foreach (List<string> entry in results)
            {
                GR5_AMM_GameMode mode = new GR5_AMM_GameMode
                {
                    uiId = Convert.ToUInt32(entry[1]),
                    uiRootModifierId = Convert.ToUInt32(entry[2]),
                    uiType = Convert.ToUInt32(entry[3]),
                    uiOasisNameId = Convert.ToUInt32(entry[4]),
                    uiOasisDescriptionId = Convert.ToUInt32(entry[5]),
                    uiThumbnailId = Convert.ToUInt32(entry[6]),
                    m_ModifierVector = new List<GR5_AMM_Modifier>()
                };
                List<List<string>> results2 = GetQueryResults("SELECT * FROM amm_modifiers WHERE listType=1 AND listID=" + mode.uiId);
                foreach (List<string> entry2 in results2)
                {
                    GR5_AMM_Modifier mm = new GR5_AMM_Modifier
                    {
                        uiId = Convert.ToUInt32(entry2[3]),
                        uiParentId = Convert.ToUInt32(entry2[4]),
                        uiType = Convert.ToUInt32(entry2[5]),
                        uiValue = entry2[6]
                    };
                    mode.m_ModifierVector.Add(mm);
                }
                result.Add(mode);
            }
            return result;
        }

        public static List<GR5_AMM_GameDetail> GetAMMGameDetails()
        {
            List<GR5_AMM_GameDetail> result = new List<GR5_AMM_GameDetail>();
            List<List<string>> results = GetQueryResults("SELECT * FROM amm_gamedetails");
            foreach (List<string> entry in results)
            {
                GR5_AMM_GameDetail detail = new GR5_AMM_GameDetail
                {
                    uiId = Convert.ToUInt32(entry[1]),
                    uiRootModifierId = Convert.ToUInt32(entry[2]),
                    uiOasisNameId = Convert.ToUInt32(entry[3]),
                    uiOasisDescriptionId = Convert.ToUInt32(entry[4]),
                    m_ModifierVector = new List<GR5_AMM_Modifier>()
                };
                List<List<string>> results2 = GetQueryResults("SELECT * FROM amm_modifiers WHERE listType=2 AND listID=" + detail.uiId);
                foreach (List<string> entry2 in results2)
                {
                    GR5_AMM_Modifier mm = new GR5_AMM_Modifier
                    {
                        uiId = Convert.ToUInt32(entry2[3]),
                        uiParentId = Convert.ToUInt32(entry2[4]),
                        uiType = Convert.ToUInt32(entry2[5]),
                        uiValue = entry2[6]
                    };
                    detail.m_ModifierVector.Add(mm);
                }
                result.Add(detail);
            }
            return result;
        }

        public static List<GR5_SKU> GetSKUs()
        {
            List<GR5_SKU> result = new List<GR5_SKU>();
            List<List<string>> results = GetQueryResults("SELECT * FROM skus");
            foreach(List<string> entry in results)
            {
                GR5_SKU sku = new GR5_SKU
                {
                    m_ID = Convert.ToUInt32(entry[1]),
                    m_Type = Convert.ToUInt32(entry[2]),
                    m_AvailableStock = Convert.ToUInt32(entry[3]),
                    m_TimeStart = Convert.ToUInt32(entry[4]),
                    m_TimeExpired = Convert.ToUInt32(entry[5]),
                    m_BuyIGCCost = Convert.ToUInt32(entry[6]),
                    m_BuyGRCashCost = Convert.ToUInt32(entry[7]),
                    m_AssetKey = Convert.ToUInt32(entry[8]),
                    m_Name = entry[9],
                    m_OasisName = Convert.ToUInt32(entry[10]),
                    m_ItemVector = new List<GR5_SKUItem>()
                };
                List<List<string>> results2 = GetQueryResults("SELECT * FROM skuitems WHERE skuid=" + sku.m_ID);
                foreach (List<string> entry2 in results2)
                {
                    GR5_SKUItem item = new GR5_SKUItem
                    {
                        m_ItemID = Convert.ToUInt32(entry2[1]),
                        m_DurabilityValue = Convert.ToUInt32(entry2[2]),
                        m_DurabilityValue2 = Convert.ToUInt32(entry2[3]),
                        m_OasisName = Convert.ToUInt32(entry2[4]),
                        m_IGCPrice = Convert.ToSingle(entry2[5]),
                        m_GRCashPrice = Convert.ToSingle(entry2[6])
                    };
                    sku.m_ItemVector.Add(item);
                }
                result.Add(sku);
            }
            return result;
        }

        public static List<GR5_Coupon> GetCoupons()
        {
            List<GR5_Coupon> result = new List<GR5_Coupon>();
            List<List<string>> results = GetQueryResults("SELECT * FROM coupons");
            foreach(List<string> entry in results)
            {
                GR5_Coupon coupon = new GR5_Coupon
                {
                    m_ID = Convert.ToUInt32(entry[1]),
                    m_SKUModifierID = Convert.ToUInt32(entry[2]),
                    m_TimeStart = Convert.ToUInt32(entry[3]),
                    m_TimeExpired = Convert.ToUInt32(entry[4])
                };
                result.Add(coupon);
            }
            return result;
        }

        public static List<GR5_SKUModifier> GetSKUModifiers()
        {
            List<GR5_SKUModifier> result = new List<GR5_SKUModifier>();
            List<List<string>> results = GetQueryResults("SELECT * FROM skumodifiers");
            foreach(List<string> entry in results)
            {
                GR5_SKUModifier mod = new GR5_SKUModifier
                {
                    m_ID = Convert.ToUInt32(entry[1]),
                    m_CouponBatchID = Convert.ToUInt32(entry[2]),
                    m_TimeStart = Convert.ToUInt32(entry[3]),
                    m_TimeExpired = Convert.ToUInt32(entry[4]),
                    m_TargetType = Convert.ToUInt32(entry[5]),
                    m_TargetValue = Convert.ToUInt32(entry[6]),
                    m_Tag = entry[7]
                };
                List<List<string>> results2 = GetQueryResults("SELECT * FROM skumodconditions WHERE modid=" + mod.m_ID);    //modid column for reference only
                foreach (List<string> entry2 in results2)
                {
                    GR5_SKUModifierCondition condition = new GR5_SKUModifierCondition();
                    condition.m_Type = Convert.ToUInt32(entry2[2]);
                    condition.m_Target = Convert.ToUInt32(entry2[3]);
                    condition.m_Value = Convert.ToUInt32(entry2[4]);
                    mod.m_ConditionVector.Add(condition);
                }
                results2.Clear();
                results2 = GetQueryResults("SELECT * FROM skumodoutput WHERE modid=" + mod.m_ID);   //same, only for reference
                foreach(List<string> entry2 in results2)
                {
                    GR5_SKUModifierOutput output = new GR5_SKUModifierOutput();
                    output.m_Type = Convert.ToUInt32(entry2[2]);
                    output.m_Target = Convert.ToUInt32(entry2[3]);
                    output.m_Value = Convert.ToUInt32(entry2[4]);
                    mod.m_OutputVector.Add(output);
                }
                result.Add(mod);
            }
            return result;
        }

        public static List<GR5_GameClass> GetGameClasses()
        {
            List<GR5_GameClass> classes = new List<GR5_GameClass>();
            List<List<string>> results = GetQueryResults("SELECT * FROM gameclasses");
            foreach (List<string> entry in results)
            {
                GR5_GameClass gclass = new GR5_GameClass
                {
                    m_ID = Convert.ToUInt32(entry[1]),
                    m_ModifierListID = Convert.ToUInt32(entry[2]),
                    m_OasisID = Convert.ToUInt32(entry[3]),
                    m_Name = entry[4],
                    m_LoadoutID = Convert.ToUInt32(entry[5])
                };
                List<uint> equipweaponids = new List<uint>();
                List<List<string>> results2 = GetQueryResults("SELECT * FROM equipweaponids WHERE classid=" + gclass.m_ID); //classid for reference
                foreach (List<string> entry2 in results2)
                {
                    equipweaponids.Add(Convert.ToUInt32(entry2[2]));
                }
                gclass.m_EquippableWeaponIDVector = equipweaponids;
                List<uint> defskillnodes = new List<uint>();
                results2.Clear();
                results2 = GetQueryResults("SELECT * FROM defskillnodes WHERE classid=" + gclass.m_ID);
                foreach (List<string> entry2 in results2)
                {
                    defskillnodes.Add(Convert.ToUInt32(entry2[2]));
                }
                gclass.m_DefaultSkillNodeIDVector = defskillnodes;
                classes.Add(gclass);
            }
            return classes;
        }

        public static List<GR5_FriendData> GetFriends(ClientInfo client)
        {
            List<GR5_FriendData> friends = new List<GR5_FriendData>();
            List<List<string>> results = GetQueryResults("SELECT * FROM friends WHERE friendofpid=" + client.PID);
            foreach (List<string> entry in results)
            {
                GR5_FriendData fd = new GR5_FriendData();
                fd.m_Person.PersonaID = Convert.ToUInt32(entry[2]);
                fd.m_Person.PersonaName = entry[3];
                fd.m_Person.PersonaStatus = GR5_BasicPersona.STATUS.Online;
                fd.m_Person.AvatarPortraitID = Convert.ToUInt32(entry[5]);
                fd.m_Person.AvatarDecoratorID = Convert.ToUInt32(entry[6]);
                fd.m_Person.AvatarBackgroundColor = Convert.ToUInt32(entry[7]);
                fd.m_Person.CurrentCharacterID = Convert.ToByte(entry[8]);
                fd.m_Person.CurrentCharacterLevel = Convert.ToByte(entry[9]);
                fd.m_Group = Convert.ToByte(entry[10]);
                friends.Add(fd);
            }
            return friends;
        }

        public static bool AddFriend(ClientInfo client, GR5_FriendData friend)
        {
            //TODO: rewrite with an ORM for sql injection and easier mapping
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO friends (friendofpid, pid, name, status, portraitid, decoratorid, background, classid, level, 'group') VALUES (" +
                client.PID + ", " +
                friend.m_Person.PersonaID + ", '" +
                friend.m_Person.PersonaName + "', " +
                friend.m_Person.PersonaStatus + ", " +
                friend.m_Person.AvatarPortraitID + ", " +
                friend.m_Person.AvatarDecoratorID + ", " +
                friend.m_Person.AvatarBackgroundColor + ", " +
                friend.m_Person.CurrentCharacterID + ", " +
                friend.m_Person.CurrentCharacterLevel + ", " +
                friend.m_Group + ");", connection);

            try { 
                return cmd.ExecuteNonQuery() > 0;
            }
            catch {
                return false;
            }
            
        }

        public static void SetAvatarPortrait(ClientInfo client, uint portraitId, uint backgroundColor)
        {
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE personas SET portraitid = {portraitId}, backcolor = {backgroundColor} WHERE pid = {client.PID};" , connection);
            try { cmd.ExecuteNonQuery(); }
            catch { return; }
        }

        public static void SetAvatarDecorator(ClientInfo client, uint decoratorId)
        {
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE personas SET decorid = {decoratorId} WHERE pid = {client.PID};", connection);
            try { cmd.ExecuteNonQuery(); }
            catch { return; }
        }

        public static List<GR5_OperatorVariable> GetOperatorVariables()
        {
            List<GR5_OperatorVariable> opVars = new List<GR5_OperatorVariable>();
            List<List<string>> results = GetQueryResults("SELECT * FROM op_vars");
            GR5_OperatorVariable opVar = new GR5_OperatorVariable();
            foreach (List<string>entry in results)
            {
                opVar.m_Id = Convert.ToUInt32(entry[1]);
                opVar.m_Value = entry[2];
                opVars.Add(opVar);
            }
            return opVars;
        }
    }
}
