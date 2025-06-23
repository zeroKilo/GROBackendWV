using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SimpleTypeDeclaration : ParseTreeItem<SimpleTypeDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.SimpleTypeDeclaration;
        [JsonProperty("decl")]
        public Declaration Declaration { get; set; } = new Declaration();
        [JsonProperty("type")]
        public string TypeName { get; set; }

        protected override SimpleTypeDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[SimpleTypeDeclaration]");
            Declaration.Parse(s, log, depth + 1);
            TypeName = Declaration.NsItem.TreeItemName;
            log.AppendLine($"{tabs}\t[type: {TypeName}]");
            return this;
        }
    }
}
