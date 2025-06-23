using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DupSpaceDeclaration : ParseTreeItem<DupSpaceDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.DupSpaceDeclaration;
        [JsonProperty("decl")]
        public Declaration Declaration { get; set; } = new Declaration();

        protected override DupSpaceDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[DupSpaceDeclaration]");
            Declaration.Parse(s, log, depth + 1);
            return this;
        }
    }
}
