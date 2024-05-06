using System;
using System.Collections.Generic;

namespace QuazalWV.DB
{
    public static class PassiveAbilityModel
    {
        public static List<GR5_PassiveAbility> GetTemplates()
        {
            List<GR5_PassiveAbility> teamUpgrades = new List<GR5_PassiveAbility>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM passiveabilities");
            foreach (var row in rows)
            {
                teamUpgrades.Add(
                    new GR5_PassiveAbility()
                    {
                        Id = Convert.ToUInt32(row[1]),
                        ClassID = Convert.ToByte(row[2]),
                        ModifierListID = Convert.ToUInt32(row[3]),
                        Type = Convert.ToUInt32(row[4]),
                        AssetKey = Convert.ToUInt32(row[5])
                    }
                );
            }
            return teamUpgrades;
        }
    }
}
