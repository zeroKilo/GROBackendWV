using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseStatisticsService_Method3 : RMCPacketReply
    {
        public class InstancedStatistic
        {
            public uint m_ID;
            public uint m_Type;
            public uint m_TID;
            public uint m_Value;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_ID);
                Helper.WriteU32(s, m_Type);
                Helper.WriteU32(s, m_TID);
                Helper.WriteU32(s, m_Value);
            }
        }

        public class PlayerInstancedStatisticsBlock
        {
            public uint m_PlayerID;
            public List<InstancedStatistic> m_StatisticVector = new List<InstancedStatistic>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, m_PlayerID);
                Helper.WriteU32(s, (uint)m_StatisticVector.Count);
                foreach (InstancedStatistic i in m_StatisticVector)
                    i.toBuffer(s);
            }
        }

        public List<PlayerInstancedStatisticsBlock> list = new List<PlayerInstancedStatisticsBlock>();

        public RMCPacketResponseStatisticsService_Method3()
        {
            PlayerInstancedStatisticsBlock b = new PlayerInstancedStatisticsBlock();
            b.m_StatisticVector.Add(new InstancedStatistic());
            list.Add(b);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (PlayerInstancedStatisticsBlock p in list)
                p.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseStatisticsService_Method3]";
        }
    }

}
