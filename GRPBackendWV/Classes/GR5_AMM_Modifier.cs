using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_AMM_Modifier
    {
        public uint uiId;
        public uint uiParentId;
        public uint uiType;
        public string uiValue;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, uiId);
            Helper.WriteU32(s, uiParentId);
            Helper.WriteU32(s, uiType);
            Helper.WriteString(s, uiValue);
        }
    }
}
