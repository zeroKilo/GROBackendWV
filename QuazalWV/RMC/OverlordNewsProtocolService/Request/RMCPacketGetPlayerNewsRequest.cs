using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuazalWV
{
    public class RMCPacketGetPlayerNewsRequest : RMCPRequest
    {
        public string PredicateName { get; private set; }
        public uint OuterSize { get; private set; }
        public uint InnerSize { get; private set; }
        public uint NbPlayers { get; private set; }
        public List<uint> Players { get; private set; }
        public uint MsgRangeFrom { get; private set; }
        public uint MsgRangeTo { get; private set; }

        public RMCPacketGetPlayerNewsRequest(Stream s, string name = "ProtoPredicatePrincipals")
        {
            PredicateName = name;
            OuterSize = Helper.ReadU32(s);
            InnerSize = Helper.ReadU32(s);
            Players = new List<uint>();
            NbPlayers = Helper.ReadU32(s);
            for (int p = 0; p < NbPlayers; p++)
                Players.Add(Helper.ReadU32(s));
            MsgRangeFrom = Helper.ReadU32(s);
            MsgRangeTo = Helper.ReadU32(s);
        }

        public override string ToString()
        {
            return MsgRangeTo == 0x40 ? $"[GetPersonaNews Request, PID={Players[0]}]" : $"[GetFriendsNews Request, NbFriends={NbPlayers}]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[Predicate  : " + PredicateName + "]");
            sb.AppendLine("\t[NbPlayers  : " + NbPlayers + "]");
            sb.AppendLine("\t[Max msgs   : " + MsgRangeTo + "]");
            return "";
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteString(m, PredicateName);
            Helper.WriteU32(m, OuterSize);
            Helper.WriteU32(m, InnerSize);
            Helper.WriteU32(m, NbPlayers);
            foreach(uint pid in Players) Helper.WriteU32(m, pid);
            Helper.WriteU32(m, MsgRangeFrom);
            Helper.WriteU32(m, MsgRangeTo);
            return m.ToArray();
        }
    }
}
