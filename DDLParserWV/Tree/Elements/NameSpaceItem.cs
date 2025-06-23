using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NameSpaceItem : ParseTreeItem<NameSpaceItem>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.NameSpaceItem;
        [JsonProperty("treeItemName")]
        public string TreeItemName { get; set; }
        [JsonProperty("nsItemName")]
        public string NsItemName { get; set; }

        protected override NameSpaceItem ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[NameSpaceItem]");
            TreeItemName = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[TreeItemName: {TreeItemName}]");
            NsItemName = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[NsItemName: {NsItemName}]");
            return this;
        }
    }
}
