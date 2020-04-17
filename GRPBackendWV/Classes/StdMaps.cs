using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class Map_U32_VectorU32
    {
        public uint key;
        public List<uint> vector = new List<uint>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, (uint)vector.Count);
            foreach (uint u in vector)
                Helper.WriteU32(s, u);
        }
    }

    public class Map_U32_VectorGR5_Weapon
    {
        public uint key;
        public List<GR5_Weapon> vector = new List<GR5_Weapon>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, (uint)vector.Count);
            foreach (GR5_Weapon w in vector)
                w.toBuffer(s);
        }
    }

    public class Map_U32_VectorGR5_Component
    {
        public uint key;
        public List<GR5_Component> vector = new List<GR5_Component>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, (uint)vector.Count);
            foreach (GR5_Component c in vector)
                c.toBuffer(s);
        }
    }
}
