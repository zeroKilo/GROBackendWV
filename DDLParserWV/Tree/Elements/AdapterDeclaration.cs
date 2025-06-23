using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AdapterDeclaration : ParseTreeItem<AdapterDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.AdapterDeclaration;
        [JsonProperty("decl")]
        public Declaration Declaration { get; set; } = new Declaration();

        protected override AdapterDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[AdapterDeclaration]");
            Declaration.Parse(s, log, depth + 1);
            return this;
        }
    }
}
