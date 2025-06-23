using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Declaration : ParseTreeItem<Declaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.Declaration;
        [JsonProperty("nsItem")]
        public NameSpaceItem NsItem { get; set; } = new NameSpaceItem();
        [JsonProperty("nsName")]
        public string NamespaceName { get; set; }
        [JsonProperty("namespace")]
        public NameSpace NameSpace { get; set; }

        protected override Declaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[Declaration]");
            NsItem.Parse(s, log, depth + 1);
            NamespaceName = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[nsName: {NamespaceName}]");
            NameSpace = new NameSpace(s, log, depth + 1);
            return this;
        }
    }
}
