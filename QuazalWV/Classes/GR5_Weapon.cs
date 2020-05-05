using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_Weapon
    {
        public uint weaponID;
        public uint classTypeID;
        public uint weaponType;
        public uint equippableClassTypeID;
        public uint flags;
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
