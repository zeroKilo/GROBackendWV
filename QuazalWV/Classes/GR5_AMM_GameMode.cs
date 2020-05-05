using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_AMM_GameMode
    {
        public uint uiId;
        public uint uiRootModifierId;
        public uint uiType;
        public uint uiOasisNameId = 70870;
        public uint uiOasisDescriptionId = 70870;
        public uint uiThumbnailId;
        public List<GR5_AMM_Modifier> m_ModifierVector = new List<GR5_AMM_Modifier>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, uiId);
            Helper.WriteU32(s, uiRootModifierId);
            Helper.WriteU32(s, uiType);
            Helper.WriteU32(s, uiOasisNameId);
            Helper.WriteU32(s, uiOasisDescriptionId);
            Helper.WriteU32(s, uiThumbnailId);
            Helper.WriteU32(s, (uint)m_ModifierVector.Count);
            foreach (GR5_AMM_Modifier mod in m_ModifierVector)
                mod.toBuffer(s);
        }
    }
}
