using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_AMM_PlaylistEntry
    {
        public uint uiMapId;
        public uint uiGameMode;
        public uint uiMatchDetail;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, uiMapId);
            Helper.WriteU32(s, uiGameMode);
            Helper.WriteU32(s, uiMatchDetail);
        }
    }
}
