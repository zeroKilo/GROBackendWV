﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStatisticsService_GetPlayerLifetimeStatistics : RMCPResponse
    {
        public List<GR5_PlayerStatisticsBlock> psb = new List<GR5_PlayerStatisticsBlock>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)psb.Count);
            foreach (GR5_PlayerStatisticsBlock p in psb)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStatisticsService_GetPlayerLifetimeStatistics]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
