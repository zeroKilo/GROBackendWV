using System;
using System.Collections.Generic;

namespace QuazalWV.DB
{
    public static class UnlockModel
    {
        public static List<GR5_Unlock> GetUnlocks()
        {
            List<GR5_Unlock> unlocks = new List<GR5_Unlock>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM unlocks");
            foreach (var row in rows)
            {
                unlocks.Add(
                    new GR5_Unlock()
                    {
                        mID = Convert.ToUInt32(row[1]),
                        mUnlockItem = Convert.ToUInt32(row[2]),
                        mUnlockType = Convert.ToByte(row[3]),
                        mClassID1 = Convert.ToUInt32(row[4]),
                        mLevel1 = Convert.ToInt32(row[5]),
                        mClassID2 = Convert.ToUInt32(row[6]),
                        mLevel2 = Convert.ToInt32(row[7]),
                        mClassID3 = Convert.ToUInt32(row[8]),
                        mLevel3 = Convert.ToInt32(row[9]),
                        mAchievementID = Convert.ToUInt32(row[10]),
                        mAchievementWallID = Convert.ToUInt32(row[11]),
                        mFactionPoint1 = Convert.ToUInt32(row[12]),
                        mFactionPoint2 = Convert.ToUInt32(row[13]),
                        mFactionPoint3 = Convert.ToUInt32(row[14]),
                        mFactionPoint4 = Convert.ToUInt32(row[15]),
                        mFactionPoint5 = Convert.ToUInt32(row[16])
                    }
                );
            }
            return unlocks;
        }
    }
}
