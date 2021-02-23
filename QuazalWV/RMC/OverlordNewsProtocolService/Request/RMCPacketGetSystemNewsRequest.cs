using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketGetSystemNewsRequest : RMCPRequest
    {
        public string PredicateName { get; private set; }
        public uint OuterSize { get; private set; }
        public uint InnerSize { get; private set; }
        public string ChannelType { get; private set; }
        public string NewsType { get; private set; }
        public uint MsgRangeFrom { get; private set; }
        public uint MsgRangeTo { get; private set; }

        public RMCPacketGetSystemNewsRequest(Stream s, string name = "ProtoPredicateGameNews")
        {
            PredicateName = name;
            OuterSize = Helper.ReadU32(s);
            InnerSize = Helper.ReadU32(s);
            ChannelType = Helper.ReadString(s);
            NewsType = Helper.ReadString(s);
            MsgRangeFrom = Helper.ReadU32(s);
            MsgRangeTo = Helper.ReadU32(s);
        }

        public override string ToString()
        {
            return $"[GetSystemNews Request, channel={ChannelType}]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[Predicate  : " + PredicateName + "]");
            sb.AppendLine("\t[Channel    : " + ChannelType + "]");
            sb.AppendLine("\t[Max msgs   : " + MsgRangeTo + "]");
            return "";
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteString(m, PredicateName);
            Helper.WriteU32(m, OuterSize);
            Helper.WriteU32(m, InnerSize);
            Helper.WriteString(m, ChannelType);
            Helper.WriteString(m, NewsType);
            Helper.WriteU32(m, MsgRangeFrom);
            Helper.WriteU32(m, MsgRangeTo);
            return m.ToArray();
        }
    }
}
