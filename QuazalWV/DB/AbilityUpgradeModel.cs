using System;
using System.Collections.Generic;

namespace QuazalWV.DB
{
    public static class AbilityUpgradeModel
    {
        public static List<GR5_AbilityUpgrade> GetUpgrades()
        {
            List<GR5_AbilityUpgrade> upgrades = new List<GR5_AbilityUpgrade>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM abilityupgrades");
            foreach (var row in rows)
            {
                upgrades.Add(
                    new GR5_AbilityUpgrade()
                    {
                        Id = Convert.ToUInt32(row[1]),
                        AbilityUpgradeType = Convert.ToByte(row[2]),
                        CompatibleAbilityType = Convert.ToByte(row[3]),
                        ModifierListID = Convert.ToUInt32(row[4])
                    }
                );
            }
            return upgrades;
        }
    }
}
