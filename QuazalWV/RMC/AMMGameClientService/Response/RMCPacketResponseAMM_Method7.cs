using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAMM_Method7 : RMCPResponse
    {
        public List<GR5_AMM_Playlist> amm_playlists = new List<GR5_AMM_Playlist>();
        public List<GR5_AMM_Map> amm_maps = new List<GR5_AMM_Map>();
        public List<GR5_AMM_GameMode> amm_gamemodes = new List<GR5_AMM_GameMode>();
        public List<GR5_AMM_GameDetail> amm_gamedetails = new List<GR5_AMM_GameDetail>();

        public RMCPacketResponseAMM_Method7()
        {
            amm_playlists = DBHelper.GetAMMPlaylists();
            amm_maps = DBHelper.GetAMMMaps();
            amm_gamemodes = DBHelper.GetAMMGameModes();
            amm_gamedetails = DBHelper.GetAMMGameDetails();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)amm_playlists.Count);
            foreach (GR5_AMM_Playlist l in amm_playlists)
                l.toBuffer(m);
            Helper.WriteU32(m, (uint)amm_maps.Count);
            foreach (GR5_AMM_Map l in amm_maps)
                l.toBuffer(m);
            Helper.WriteU32(m, (uint)amm_gamemodes.Count);
            foreach (GR5_AMM_GameMode l in amm_gamemodes)
                l.toBuffer(m);
            Helper.WriteU32(m, (uint)amm_gamedetails.Count);
            foreach (GR5_AMM_GameDetail l in amm_gamedetails)
                l.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAMM_Method7]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
