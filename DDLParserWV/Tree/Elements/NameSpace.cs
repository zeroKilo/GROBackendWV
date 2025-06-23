using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NameSpace
    {
        [JsonProperty("count")]     
        public uint Count { get; set; }
        [JsonProperty("items")]
        public List<ParseTreeItemBase> Items { get; set; } = new List<ParseTreeItemBase>();

        public NameSpace(Stream s, StringBuilder log, uint depth)
        {
            string tabs = Utils.MakeTabs(depth);
            log.AppendLine($"{tabs}[NameSpace]");
            Count = Utils.ReadU32(s);
            for (int i = 0; i < Count; i++)
            {
                byte type = (byte)s.ReadByte();
                switch ((EParseTreeElement)type)
                {
                    case EParseTreeElement.NameSpaceItem:
                        Items.Add(new NameSpaceItem().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.Declaration:
                        Items.Add(new Declaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.DOClassDeclaration:
                        Items.Add(new DOClassDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.DatasetDeclaration:
                        Items.Add(new DatasetDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.TypeDeclaration:
                        Items.Add(new TypeDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.Variable:
                        Items.Add(new Variable().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.MethodDeclaration:
                        Items.Add(new MethodDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.RMC:
                        Items.Add(new RMC().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.Action:
                        Items.Add(new Action().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.AdapterDeclaration:
                        Items.Add(new AdapterDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.PropertyDeclaration:
                        Items.Add(new PropertyDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.ProtocolDeclaration:
                        Items.Add(new ProtocolDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.Parameter:
                        Items.Add(new Parameter().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.ReturnValue:
                        Items.Add(new ReturnValue().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.ClassDeclaration:
                        Items.Add(new ClassDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.TemplateDeclaration:
                        Items.Add(new TemplateDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.SimpleTypeDeclaration:
                        Items.Add(new SimpleTypeDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.TemplateInstance:
                        Items.Add(new TemplateInstance().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.DDLUnitDeclaration:
                        Items.Add(new DDLUnitDeclaration().Parse(s, log, depth + 1));
                        break;
                    case EParseTreeElement.DupSpaceDeclaration:
                        Items.Add(new DupSpaceDeclaration().Parse(s, log, depth + 1));
                        break;
                    default:
                        throw new Exception($"Unknown NameSpaceItem type {type}");
                }
            }
        }
    }
}
