using System;
using System.Collections.Generic;

namespace QuazalWV.DB
{
    public static class AdModel
    {
        public static List<GR5_Advertisement> GetAds()
        {
            List<GR5_Advertisement> ads = new List<GR5_Advertisement>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM ads");
            foreach (var row in rows)
            {
                ads.Add(
                    new GR5_Advertisement()
                    {
                        m_ID = Convert.ToUInt32(row[1]),
                        m_StoreItemID = Convert.ToUInt32(row[2]),
                        m_AssetId = Convert.ToUInt32(row[3]),
                        m_Layout = Convert.ToByte(row[4]),
                        m_Action = Convert.ToByte(row[5]),
                        m_Criteria = row[6]
                    }
                );
            }
            return ads;
        }

        public static List<GR5_AdServer> GetAdServers()
        {
            List<GR5_AdServer> servers = new List<GR5_AdServer>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM adservers");
            foreach (var row in rows)
            {
                servers.Add(
                    new GR5_AdServer()
                    {
                        m_Id = Convert.ToUInt32(row[1]),
                        m_Type = Convert.ToByte(row[2]),
                        m_DesignerName = row[3]
                    }
                );
            }
            return servers;
        }

        public static List<GR5_AdRecommender> GetAdRecommenders()
        {
            List<GR5_AdRecommender> recommenders = new List<GR5_AdRecommender>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM adrecommenders");
            foreach (var row in rows)
            {
                recommenders.Add(
                    new GR5_AdRecommender()
                    {
                        m_AdServerId = Convert.ToUInt32(row[1]),
                        m_AdCount = Convert.ToUInt32(row[2])
                    }
                );
            }
            return recommenders;
        }

        public static List<GR5_AdContainer> GetAdContainers()
        {
            List<GR5_AdContainer> containers = new List<GR5_AdContainer>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM adcontainers");
            foreach (var row in rows)
            {
                containers.Add(
                    new GR5_AdContainer()
                    {
                        Id = Convert.ToUInt32(row[1]),
                        AdServerId = Convert.ToUInt32(row[2]),
                        DesignerName = row[3],
                        AdInterval = Convert.ToByte(row[4]),
                        ContainerLocation = Convert.ToByte(row[5])
                    }
                );
            }
            return containers;
        }

        public static List<GR5_AdStaticList> GetAdStaticLists()
        {
            List<GR5_AdStaticList> lists = new List<GR5_AdStaticList>();
            var rows = DBHelper.GetQueryResults("SELECT * FROM adstaticlists");
            foreach (var row in rows)
            {
                lists.Add(
                    new GR5_AdStaticList()
                    {
                        m_AdServerId = Convert.ToUInt32(row[1]),
                        m_AdvertId = Convert.ToUInt32(row[2]),
                        m_AdType = Convert.ToByte(row[3]),
                        m_Priority = Convert.ToByte(row[4])
                    }
                );
            }
            return lists;
        }
    }
}
