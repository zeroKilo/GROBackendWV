using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TypeDeclaration : ParseTreeItem<TypeDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.TypeDeclaration;
        [JsonProperty("decl")]
        public Declaration Declaration { get; set; } = new Declaration();

        protected override TypeDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[TypeDeclaration]");
            Declaration.Parse(s, log, depth + 1);
            return this;
        }
    }
}
