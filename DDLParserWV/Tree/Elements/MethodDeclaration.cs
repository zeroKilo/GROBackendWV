using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MethodDeclaration : ParseTreeItem<MethodDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.MethodDeclaration;
        [JsonProperty("decl")]
        public Declaration Declaration { get; set; } = new Declaration();
        [JsonProperty("namespace")]
        public NameSpace NameSpace { get; set; }

        protected override MethodDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[MethodDeclaration]");
            Declaration.Parse(s, log, depth + 1);
            NameSpace = new NameSpace(s, log, depth + 1);
            return this;
        }
    }
}
