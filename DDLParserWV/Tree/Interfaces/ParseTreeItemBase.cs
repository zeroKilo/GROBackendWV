using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ParseTreeItemBase
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("elemType")]
        public abstract EParseTreeElement Type { get; set; }
        public abstract ParseTreeItemBase Parse(Stream s, StringBuilder log, uint depth);
    }
}
