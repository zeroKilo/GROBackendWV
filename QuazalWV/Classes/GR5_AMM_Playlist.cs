using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_AMM_Playlist
    {
        public uint uiId;
        public uint uiNodeType;
        public uint uiMaxTeamSize;
        public uint uiMinTeamSize;
        public uint uiOasisNameId = 70870;
        public uint uiOasisDescriptionId = 70870;
        public uint uiIsRepeatable;
        public uint uiIsRandom;
        public uint uiThumbnailId;
        public List<GR5_AMM_PlaylistEntry> m_PlaylistEntryVector = new List<GR5_AMM_PlaylistEntry>();
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, uiId);
            Helper.WriteU32(s, uiNodeType);
            Helper.WriteU32(s, uiMaxTeamSize);
            Helper.WriteU32(s, uiMinTeamSize);
            Helper.WriteU32(s, uiOasisNameId);
            Helper.WriteU32(s, uiOasisDescriptionId);
            Helper.WriteU32(s, uiIsRepeatable);
            Helper.WriteU32(s, uiIsRandom);
            Helper.WriteU32(s, uiThumbnailId);
            Helper.WriteU32(s, (uint)m_PlaylistEntryVector.Count);
            foreach (GR5_AMM_PlaylistEntry e in m_PlaylistEntryVector)
                e.toBuffer(s);
        }
    }
}
