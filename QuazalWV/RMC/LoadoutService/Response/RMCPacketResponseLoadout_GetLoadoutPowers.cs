using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseLoadout_GetLoadoutPowers : RMCPResponse
    {
        public class GR5_Power
        {
            public uint id;
            public uint value;

            public GR5_Power(uint val)
            {
                id = val;
                value = val;
            }

            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, id);
                Helper.WriteU32(s, value);
            }
        }

        public List<GR5_Power> list = new List<GR5_Power>();

        public RMCPacketResponseLoadout_GetLoadoutPowers()
        {
            for(uint i = 0; i < 6; i++)
                list.Add(new GR5_Power(i));
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_Power u in list)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseLoadout_GetLoadoutPowers]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
