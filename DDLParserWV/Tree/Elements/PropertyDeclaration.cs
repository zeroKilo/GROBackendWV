using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PropertyDeclaration : ParseTreeItem<PropertyDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.PropertyDeclaration;
        [JsonProperty("decl")]
        public Declaration Declaration {  get; set; } = new Declaration();
        [JsonProperty("categoryFlags")]
        public uint CategoryFlags { get; set; }
        [JsonProperty("targetFlags")]
        public uint TargetFlags { get; set; }

        protected override PropertyDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[PropertyDeclaration]");
            Declaration.Parse(s, log, depth + 1);
            CategoryFlags = Utils.ReadU32(s);
            log.AppendLine($"{tabs}\t[categoryFlags: {CategoryFlags}]");
            TargetFlags = Utils.ReadU32(s);
            log.AppendLine($"{tabs}\t[targetFlags: {TargetFlags}]");
            return this;
        }
    }
}
