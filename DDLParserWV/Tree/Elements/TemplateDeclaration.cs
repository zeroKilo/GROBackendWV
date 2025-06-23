using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TemplateDeclaration : ParseTreeItem<TemplateDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.TemplateDeclaration;
        [JsonProperty("typeDecl")]
        public Declaration TypeDeclaration { get; set; } = new Declaration();
        [JsonProperty("unknown")]
        public uint Unknown {  get; set; }

        protected override TemplateDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[TemplateDeclaration]");
            TypeDeclaration.Parse(s, log, depth + 1);
            Unknown = Utils.ReadU32(s);
            log.AppendLine($"{tabs}\t[unknown: {Unknown}]");
            return this;
        }
    }
}
