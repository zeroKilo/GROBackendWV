using System;
using System.Collections.Generic;

namespace QuazalWV.DB
{
    public static class ConsumableModel
    {
        public static List<GR5_Consumable> GetConsumables()
        {
            List<GR5_Consumable> consumables = new List<GR5_Consumable>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM consumables");
            foreach (var row in rows)
            {
                consumables.Add(
                    new GR5_Consumable()
                    {
                        m_ItemID = Convert.ToUInt32(row[1]),
                        m_AssetKey = Convert.ToUInt32(row[2]),
                        m_Type = Convert.ToUInt32(row[3]),
                        m_Value1 = Convert.ToUInt32(row[4]),
                        m_Value2 = Convert.ToUInt32(row[5]),
                        m_Name = row[6]
                    }
                );
            }
            return consumables;
        }
    }
}
