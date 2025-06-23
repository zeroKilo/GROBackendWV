using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DeclarationUse
    {
        [JsonProperty("type")]
        public string TypeName { get; set; }

        public DeclarationUse(Stream s, EParseTreeElement type, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[DeclarationUse]");
            if (type == EParseTreeElement.TemplateInstance)
                TypeName = new TemplateDeclarationUse().Parse(s, log, depth + 1).NsItem.TreeItemName;
            // SimpleTypeDeclaration
            else
                TypeName = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[type: {TypeName}]");
        }
    }
}
