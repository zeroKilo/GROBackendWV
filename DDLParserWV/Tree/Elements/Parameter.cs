using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Parameter : ParseTreeItem<Parameter>
    {
        public override EParseTreeElement Type { get; set; } = EParseTreeElement.Parameter;
        [JsonProperty("var")]
        public Variable Variable { get; set; } = new Variable();
        [JsonProperty("declUse")]
        public DeclarationUse DeclarationUse {  get; set; }
        [JsonProperty("arraySize")]
        public uint ArraySize { get; set; }
        [JsonIgnore]
        public byte Flags { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }

        protected override Parameter ParseTyped(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[Parameter]");
            Variable.Parse(s, log, depth + 1);
            byte type = (byte)s.ReadByte();
            DeclarationUse = new DeclarationUse(s, (EParseTreeElement)type, log, depth + 1);
            ArraySize = Utils.ReadU32(s);
            log.AppendLine($"{tabs}\t[arraySize: {ArraySize}]");
            Flags = (byte)s.ReadByte();
            switch (Flags & 3)
            {
                case 1:
                    Direction = "in";
                    break;
                case 2:
                    Direction = "out";
                    break;
                case 3:
                    Direction = "in/out";
                    break;
            }
            log.AppendLine($"{tabs}\t[direction: {Direction}]");
            return this;
        }

        public bool IsRequest()
        {
            return Flags == 1 || Flags == 3;
        }

        public bool IsResponse()
        {
            return Flags == 2 || Flags == 3;
        }

        public string GetName()
        {
            return Variable.NsItem.NsItemName;
        }

        public string GetFullType()
        {
            return DeclarationUse.TypeName;
        }
    }
}
