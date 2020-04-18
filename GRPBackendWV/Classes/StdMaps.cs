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
            Helper.WriteU32(s, key);
            Helper.WriteU32(s, (uint)vector.Count);
            foreach (uint u in vector)
                Helper.WriteU32(s, u);
        }
    }

    public class Map_U32_GR5_Weapon
    {
        public uint key;
        public GR5_Weapon weapon;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, key);
            weapon.toBuffer(s);
        }
    }

    public class Map_U32_GR5_Component
    {
        public uint key;
        public GR5_Component component;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, key);
            component.toBuffer(s);
        }
    }
}
