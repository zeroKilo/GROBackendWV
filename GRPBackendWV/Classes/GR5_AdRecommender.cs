using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_AdRecommender
    {
        public uint unk1;
        public uint unk2;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, unk1);
            Helper.WriteU32(s, unk2);
        }
    }
}
