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
            foreach (var tree in ParseTrees)
            {
                DDLUnitDeclaration ddlUnit = new DDLUnitDeclaration();
                List<ProtocolDeclaration> protocols = new List<ProtocolDeclaration>();
                List<ClassDeclaration> classes = new List<ClassDeclaration>();
                foreach (var item in tree.GlobalNamespace.Items)
                {
                    if (item.Type == EParseTreeElement.DDLUnitDeclaration)
                        ddlUnit = (DDLUnitDeclaration)item;
                    else if (item.Type == EParseTreeElement.ProtocolDeclaration)
                        protocols.Add((ProtocolDeclaration)item);
                    else if (item.Type == EParseTreeElement.ClassDeclaration)
                        classes.Add((ClassDeclaration)item);
                }

                foreach (var protocol in protocols)
                    md += MarkdownRenderer.RenderProtocol(protocol);

                if (protocols.Count == 0)
                    md += $"# {ddlUnit.UnitName}\n";

                md += MarkdownRenderer.RenderClasses(classes);
            }
            return md;
        }
    }
}
