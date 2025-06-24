using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ClassDeclaration : ParseTreeItem<ClassDeclaration>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.ClassDeclaration;
        [JsonProperty("typeDecl")]
        public Declaration TypeDeclaration { get; set; } = new Declaration();
        [JsonProperty("parentNamespace")]
        public string ParentNamespaceName { get; set; }
        [JsonProperty("namespace")]
        public NameSpace NameSpace { get; set; }

        protected override ClassDeclaration ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[ClassDeclaration]");
            TypeDeclaration.Parse(s, log, depth + 1);
            ParentNamespaceName = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[parentNamespace: {ParentNamespaceName}]");
            NameSpace = new NameSpace(s, log, depth + 1);
            return this;
        }

        public string GetName()
        {
            return TypeDeclaration.NsItem.NsItemName;
        }
    }
}
