using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ParseTree
    {
        /// <summary>
        /// Unknown byte, usually 0.
        /// </summary>
        [JsonProperty("unusedByte")]
        public byte UnusedByte { get; set; }
        /// <summary>
        /// Semver with build id added at the end.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("namespace")]
        public NameSpace GlobalNamespace {  get; set; }

        public ParseTree(Stream s, StringBuilder log)
        {
            log.AppendLine("[ParseTree]");
            UnusedByte = (byte)s.ReadByte();
            log.AppendLine($"\t[unusedByte: {UnusedByte}]");
            uint major = Utils.ReadU32(s);
            uint minor = Utils.ReadU32(s);
            uint patch = Utils.ReadU32(s);
            uint build = Utils.ReadU32(s);
            Version = $"{major}.{minor}.{patch}.{build}";
            log.AppendLine($"\t[version: {Version}]");
            GlobalNamespace = new NameSpace(s, log, 1);
            while ((s.Position % 4) != 0)
                s.ReadByte();
        }
    }
}
