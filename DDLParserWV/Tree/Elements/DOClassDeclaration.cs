using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DOClassDeclaration : ParseTreeItem<DOClassDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.DOClassDeclaration;
        [JsonProperty("decl")]
        public Declaration Declaration { get; set; } = new Declaration();
        [JsonProperty("parentDeclNamespace")]
        public string ParentDeclNamespace { get; set; }
        [JsonProperty("id")]
        public uint Id { get; set; }
        [JsonProperty("namespace")]
        public NameSpace NameSpace { get; set; }

        protected override DOClassDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[DOClassDeclaration]");
            Declaration.Parse(s, log, depth + 1);
            ParentDeclNamespace = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[parentDeclNamespace: {ParentDeclNamespace}]");
            Id = Utils.ReadU32(s);
            log.AppendLine($"{tabs}\t[id: {Id}]");
            NameSpace = new NameSpace(s, log, depth + 1);
            return this;
        }
    }
}
