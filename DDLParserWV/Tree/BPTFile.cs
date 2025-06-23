using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DDLParserWV
{
    /// <summary>
    /// The root object of output JSON.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BPTFile
    {
        [JsonProperty("file")]
        public string Name { get; set; }
        [JsonProperty("trees")]
        public List<ParseTree> ParseTrees { get; set; } = new List<ParseTree>();

        public BPTFile(string name)
        {
            Name = name;
        }

        public string ToJson()
        {
            string json;
            try
            {
                json = JsonConvert.SerializeObject(this, Formatting.Indented);
            }
            catch (Exception ex)
            {
                json = $"[ERROR] {ex}";
            }
            return json;
        }

        public string ToMarkdown()
        {
            string md = "";
            List<ProtocolDeclaration> protocols = new List<ProtocolDeclaration>();
            foreach (var tree in ParseTrees)
            {
                foreach (var item in tree.GlobalNamespace.Items)
                {
                    if (item.Type == EParseTreeElement.ProtocolDeclaration)
                        protocols.Add((ProtocolDeclaration)item);   
                }
            }

            foreach (var protocol in protocols)
                md += MarkdownRenderer.RenderProtocol(protocol);
            return md;
        }
    }
}
