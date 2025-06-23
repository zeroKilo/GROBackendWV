using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TemplateType
    {
        [JsonProperty("genericType")]
        public string GenericType {  get; set; }
        [JsonProperty("typeParams")]
        public List<string> TypeParams {  get; set; } = new List<string>();

        public TemplateType Parse(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[TemplateType");
            GenericType = Utils.ReadString(s);
            log.AppendLine($"{tabs}\t[genericType: {GenericType}");
            uint count = Utils.ReadU32(s);
            for (uint i = 0; i < count; i++)
                TypeParams.Add(Utils.ReadString(s));
            log.AppendLine($"{tabs}\t[TypeParams]");
            foreach (string param in TypeParams)
                log.AppendLine($"{tabs}\t\t[TypeParam: {param}");
            return this;
        }
    }
}
