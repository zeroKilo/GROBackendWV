using System;
using System.Collections.Generic;

namespace QuazalWV.DB
{
    public static class BoostModel
    {
        public static List<GR5_Boost> GetBoosts()
        {
            List<GR5_Boost> boosts = new List<GR5_Boost>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM boosts");
            foreach (var row in rows)
            {
                boosts.Add(
                    new GR5_Boost()
                    {
                        m_ItemID = Convert.ToUInt32(row[1]),
                        m_AssetKey = Convert.ToUInt32(row[2]),
                        m_ModifierList = Convert.ToUInt32(row[3]),
                        m_Type = Convert.ToUInt32(row[4]),
                        m_Name = row[5]
                    }
                );
            }
            return boosts;
        }
    }
}
