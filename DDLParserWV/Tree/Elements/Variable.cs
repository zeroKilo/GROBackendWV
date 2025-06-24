using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Variable : ParseTreeItem<Variable>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.Variable;
        [JsonProperty("nsItem")]
        public NameSpaceItem NsItem { get; set; } = new NameSpaceItem();
        [JsonProperty("declUse")]
        public DeclarationUse DeclarationUse { get; set; }
        [JsonProperty("arraySize")]
        public uint ArraySize { get; set; }

        protected override Variable ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[Variable]");
            NsItem.Parse(s, log, depth + 1);
            byte declUseType = (byte)s.ReadByte();
            DeclarationUse = new DeclarationUse(s, (EParseTreeElement)declUseType, log, depth + 1);
            ArraySize = Utils.ReadU32(s);
            log.AppendLine($"{tabs}\t[arraySize: {ArraySize}]");
            return this;
        }

        public string GetName()
        {
            return NsItem.NsItemName;
        }

        public string GetFullType()
        {
            return DeclarationUse.TypeName;
        }
    }
}
