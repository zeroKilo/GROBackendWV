using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_FaceSkinTone
    {
        public uint id;
        public byte objectType;
        public uint objectKey;
        public uint oasisName = 70870;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, id);
            Helper.WriteU8(s, objectType);
            Helper.WriteU32(s, objectKey);
            Helper.WriteU32(s, oasisName);
        }
    }
}
