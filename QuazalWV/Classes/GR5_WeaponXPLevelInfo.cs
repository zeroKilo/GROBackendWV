using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_WeaponXPLevelInfo
    {
        public uint id;
        public uint xp;
        public uint level;
        public uint weaponClass;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, id);
            Helper.WriteU32(s, xp);
            Helper.WriteU32(s, level);
            Helper.WriteU32(s, weaponClass);
        }
    }
}
