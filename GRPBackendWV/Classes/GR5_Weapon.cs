using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Weapon
    {
        public uint weaponID = 0x112;
        public uint classTypeID = 0x113;
        public uint weaponType = 0x114;
        public uint equippableClassTypeID = 0x115;
        public uint flags = 0x116;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, weaponID);
            Helper.WriteU32(s, classTypeID);
            Helper.WriteU32(s, weaponType);
            Helper.WriteU32(s, equippableClassTypeID);
            Helper.WriteU32(s, flags);
        }
    }
}
