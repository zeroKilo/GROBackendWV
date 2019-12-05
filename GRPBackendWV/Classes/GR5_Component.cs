using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Component
    {
        public uint _listIndex;
        public uint componentID;
        public uint componentKey;
        public byte componentType;
        public uint boneStructure;
        public uint modifierListID;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, componentID);
            Helper.WriteU32(s, componentKey);
            Helper.WriteU8(s, componentType);
            Helper.WriteU32(s, boneStructure);
            Helper.WriteU32(s, modifierListID);
        }
    }
}
