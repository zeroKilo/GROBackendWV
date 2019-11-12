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
            public uint[] unk1 = new uint[3];
            public void toBuffer(Stream s)
            {
                for (int i = 0; i < 3; i++)
                    Helper.WriteU32(s, unk1[i]);
            }
        }

        public class AMM_Playlist
        {
            public uint[] unk1 = new uint[9];
            public List<AMM_PlaylistEntry> entries = new List<AMM_PlaylistEntry>();
            public void toBuffer(Stream s)
            {
                for (int i = 0; i < 9; i++)
                    Helper.WriteU32(s, unk1[i]);
                Helper.WriteU32(s, (uint)entries.Count);
                foreach (AMM_PlaylistEntry e in entries)
                    e.toBuffer(s);
            }
        }

        public class AMM_Modifier
        {
            public uint[] unk1 = new uint[4];
            public void toBuffer(Stream s)
            {
                for (int i = 0; i < 4; i++)
                    Helper.WriteU32(s, unk1[i]);
            }
        }

        public class AMM_Map
        {
            public uint[] unk1 = new uint[6];
            public List<AMM_Modifier> mods = new List<AMM_Modifier>();
            public void toBuffer(Stream s)
            {
                for (int i = 0; i < 6; i++)
                    Helper.WriteU32(s, unk1[i]);
                Helper.WriteU32(s, (uint)mods.Count);
                foreach (AMM_Modifier mod in mods)
                    mod.toBuffer(s);
            }
        }

        public class AMM_GameMode
        {
            public uint[] unk1 = new uint[6];
            public List<AMM_Modifier> mods = new List<AMM_Modifier>();
            public void toBuffer(Stream s)
            {
                for (int i = 0; i < 6; i++)
                    Helper.WriteU32(s, unk1[i]);
                Helper.WriteU32(s, (uint)mods.Count);
                foreach (AMM_Modifier mod in mods)
                    mod.toBuffer(s);
            }
        }

        public class AMM_GameDetail
        {
            public uint[] unk1 = new uint[4];
            public List<AMM_Modifier> mods = new List<AMM_Modifier>();
            public void toBuffer(Stream s)
            {
                for (int i = 0; i < 4; i++)
                    Helper.WriteU32(s, unk1[i]);
                Helper.WriteU32(s, (uint)mods.Count);
                foreach (AMM_Modifier mod in mods)
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
