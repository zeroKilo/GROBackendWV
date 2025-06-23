using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DDLUnitDeclaration : ParseTreeItem<DDLUnitDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.DDLUnitDeclaration;
        [JsonProperty("decl")]
        public Declaration Declaration { get; set; } = new Declaration();
        [JsonProperty("unitName")]
        public string UnitName { get; set; }
        [JsonProperty("unitDir")]
        public string UnitDir { get; set; }

        protected override DDLUnitDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[DDLUnitDeclaration]");
            Declaration.Parse(s, log, depth + 1);
            UnitName = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[unitName: {UnitName}]");
            UnitDir = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[unitDir: {UnitDir}]");
            return this;
        }
    }
}
