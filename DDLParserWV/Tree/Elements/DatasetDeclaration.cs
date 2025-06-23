using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DatasetDeclaration : ParseTreeItem<DatasetDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.DatasetDeclaration;
        [JsonProperty("decl")]
        public Declaration Declaration { get; set; } = new Declaration();
        [JsonProperty("namespace")]
        public NameSpace NameSpace { get; set; }

        protected override DatasetDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[DatasetDeclaration]");
            Declaration.Parse(s, log, depth + 1);
            NameSpace = new NameSpace(s, log, depth + 1);
            return this;
        }
    }
}
