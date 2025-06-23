using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RMC : ParseTreeItem<RMC>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.RMC;
        [JsonProperty("methodDecl")]
        public MethodDeclaration MethodDeclaration { get; set; } = new MethodDeclaration();
        [JsonProperty("namespace")]
        public NameSpace NameSpace { get; set; }

        protected override RMC ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[RMC]");
            MethodDeclaration.Parse(s, log, depth + 1);
            NameSpace = new NameSpace(s, log, depth + 1);
            return this;
        }

        public string GetName()
        {
            return MethodDeclaration.Declaration.NsItem.NsItemName;
        }
    }
}
