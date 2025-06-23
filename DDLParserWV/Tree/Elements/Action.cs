using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Action : ParseTreeItem<Action>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.Action;
        [JsonProperty("methodDecl")]
        public MethodDeclaration MethodDeclaration { get; set; } = new MethodDeclaration();
        [JsonProperty("namespace")]
        public NameSpace NameSpace { get; set; }

        protected override Action ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[Action]");
            MethodDeclaration.Parse(s, log, depth + 1);
            NameSpace = new NameSpace(s, log, depth + 1);
            return this;
        }
    }
}
