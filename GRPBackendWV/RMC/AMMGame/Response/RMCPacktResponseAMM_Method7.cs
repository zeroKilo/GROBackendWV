using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacktResponseAMM_Method7 : RMCPacketReply
    {
        public class AMM_PlaylistEntry
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

        public class AMM_Playlist
        {
            public uint uiId;
            public uint uiNodeType;
            public uint uiMaxTeamSize;
            public uint uiMinTeamSize;
            public uint uiOasisNameId;
            public uint uiOasisDescriptionId;
            public uint uiIsRepeatable;
            public uint uiIsRandom;
            public uint uiThumbnailId;
            public List<AMM_PlaylistEntry> m_PlaylistEntryVector = new List<AMM_PlaylistEntry>();
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
                foreach (AMM_PlaylistEntry e in m_PlaylistEntryVector)
                    e.toBuffer(s);
            }
        }

        public class AMM_Modifier
        {
            public uint uiId;
            public uint uiParentId;
            public uint uiType;
            public uint uiValue;
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, uiId);
                Helper.WriteU32(s, uiParentId);
                Helper.WriteU32(s, uiType);
                Helper.WriteU32(s, uiValue);
            }
        }

        public class AMM_Map
        {
            public uint uiId;
            public uint uiRootModifierId;
            public uint uiMapKey;
            public uint uiOasisNameId;
            public uint uiOasisDescriptionId;
            public uint uiThumbnailId;
            public List<AMM_Modifier> m_ModifierVector = new List<AMM_Modifier>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, uiId);
                Helper.WriteU32(s, uiRootModifierId);
                Helper.WriteU32(s, uiMapKey);
                Helper.WriteU32(s, uiOasisNameId);
                Helper.WriteU32(s, uiOasisDescriptionId);
                Helper.WriteU32(s, uiThumbnailId);
                Helper.WriteU32(s, (uint)m_ModifierVector.Count);
                foreach (AMM_Modifier mod in m_ModifierVector)
                    mod.toBuffer(s);
            }
        }

        public class AMM_GameMode
        {
            public uint uiId;
            public uint uiRootModifierId;
            public uint uiType;
            public uint uiOasisNameId;
            public uint uiOasisDescriptionId;
            public uint uiThumbnailId;
            public List<AMM_Modifier> m_ModifierVector = new List<AMM_Modifier>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, uiId);
                Helper.WriteU32(s, uiRootModifierId);
                Helper.WriteU32(s, uiType);
                Helper.WriteU32(s, uiOasisNameId);
                Helper.WriteU32(s, uiOasisDescriptionId);
                Helper.WriteU32(s, uiThumbnailId);
                Helper.WriteU32(s, (uint)m_ModifierVector.Count);
                foreach (AMM_Modifier mod in m_ModifierVector)
                    mod.toBuffer(s);
            }
        }

        public class AMM_GameDetail
        {
            public uint uiId;
            public uint uiRootModifierId;
            public uint uiOasisNameId;
            public uint uiOasisDescriptionId;
            public List<AMM_Modifier> m_ModifierVector = new List<AMM_Modifier>();
            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, uiId);
                Helper.WriteU32(s, uiRootModifierId);
                Helper.WriteU32(s, uiOasisNameId);
                Helper.WriteU32(s, uiOasisDescriptionId);
                Helper.WriteU32(s, (uint)m_ModifierVector.Count);
                foreach (AMM_Modifier mod in m_ModifierVector)
                    mod.toBuffer(s);
            }
        }

        public List<AMM_Playlist> amm_playlists = new List<AMM_Playlist>();
        public List<AMM_Map> amm_maps = new List<AMM_Map>();
        public List<AMM_GameMode> amm_gamemodes = new List<AMM_GameMode>();
        public List<AMM_GameDetail> amm_gamedetails = new List<AMM_GameDetail>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)amm_playlists.Count);
            foreach (AMM_Playlist l in amm_playlists)
                l.toBuffer(m);
            Helper.WriteU32(m, (uint)amm_maps.Count);
            foreach (AMM_Map l in amm_maps)
                l.toBuffer(m);
            Helper.WriteU32(m, (uint)amm_gamemodes.Count);
            foreach (AMM_GameMode l in amm_gamemodes)
                l.toBuffer(m);
            Helper.WriteU32(m, (uint)amm_gamedetails.Count);
            foreach (AMM_GameDetail l in amm_gamedetails)
                l.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAMM_Method7]";
        }
    }
}
