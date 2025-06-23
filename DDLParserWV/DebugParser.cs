using System;
using System.IO;
using System.Text;

namespace DDLParserWV
{
    /// <summary>
    /// Legacy parser, has been incorporated into BPTFile and underlying tree elements for error debugging purposes.
    /// </summary>
    public static class DebugParser
    {
        private static StringBuilder Output { get; set; } = new StringBuilder();

        public static void Log(string s)
        {
            Output.AppendLine(s);
        }

        public static void ParseBPT(Stream m)
        {
            uint magic = Utils.ReadU32(m);
            if (magic == Utils.BPT_MAGIC)
            {
                // unused, usually 0
                byte _ = (byte)m.ReadByte();
                uint major = Utils.ReadU32(m);
                uint minor = Utils.ReadU32(m);
                uint patch = Utils.ReadU32(m);
                uint build = Utils.ReadU32(m);
                Log($"BPT version: {major}.{minor}.{patch}.{build}");
                ParseNameSpace(m);
                while ((m.Position % 4) != 0)
                    m.ReadByte();
            }
        }

        public static void ParseNameSpace(Stream m, uint depth = 0)
        {
            uint count;
            string tabs = Utils.MakeTabs(depth);
            count = Utils.ReadU32(m);
            for (int i = 0; i < count; i++)
            {
                byte type = (byte)m.ReadByte();
                switch (type)
                {
                    case 3:
                        ParseDOClassDeclaration(m, depth);
                        break;
                    case 4:
                        ParseDatasetDeclaration(m, depth);
                        break;
                    case 6:
                        ParseVariable(m, depth);
                        break;
                    case 8:
                        ParseRMC(m, depth);
                        break;
                    case 9:
                        ParseAction(m, depth);
                        break;
                    case 0xA:
                        ParseAdapterDeclaration(m, depth);
                        break;
                    case 0xB:
                        ParsePropertyDeclaration(m, depth);
                        break;
                    case 0xC:
                        ParseProtocolDeclaration(m, depth);
                        break;
                    case 0xD:
                        ParseParameter(m, depth);
                        break;
                    case 0xE:
                        ParseReturnValue(m, depth);
                        break;
                    case 0xF:
                        ParseClassDeclaration(m, depth);
                        break;
                    case 0x10:
                        ParseTemplateDeclaration(m, depth);
                        break;
                    case 0x11:
                        ParseSimpleTypeDeclaration(m, depth);
                        break;
                    case 0x12:
                        ParseTemplateInstance(m, depth);
                        break;
                    case 0x13:
                        ParseDDLUnitDeclaration(m, depth);
                        break;
                    case 0x14:
                        ParseDupSpaceDeclaration(m, depth);
                        break;
                    default:
                        Log("Unknown type found: 0x" + type.ToString("X2"));
                        throw new Exception();
                }
            }
        }

        public static void ParseDDLUnitDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[DDL Unit Declaration]");
            ParseDeclaration(m, depth + 1);
            Log(tabs + "\t[" + Utils.ReadString(m) + "]");
            Log(tabs + "\t[" + Utils.ReadString(m) + "]");
        }

        public static void ParseDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Declaration]");
            ParseNamespaceItem(m, depth + 1);
            ParseDeclarationNamespace(m, depth + 1);
        }

        public static void ParseNamespaceItem(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Namespace Item]");
            Log(tabs + $"\t[treeItemName: {Utils.ReadString(m)}]");
            Log(tabs + $"\t[nsItemName: {Utils.ReadString(m)}]");
        }

        public static void ParseDeclarationNamespace(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Namespace]");
            Log(tabs + "\t[" + Utils.ReadString(m) + "]");
            ParseNameSpace(m, depth + 1);
        }

        public static void ParseTemplateInstance(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Template Instance]");
            ParseDeclaration(m, depth + 1);
            ParseTemplateType(m, depth + 1);
        }

        public static void ParseTemplateType(Stream m, uint depth = 0)
        {
            uint count;
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[TemplateType]");
            Log(tabs + "\t[" + Utils.ReadString(m) + "]");
            count = Utils.ReadU32(m);
            Log(tabs + "\t[0x" + count.ToString("X8") + "]");
            for (int i = 0; i < count; i++)
                Log(tabs + "\t\t[" + Utils.ReadString(m) + "]");
        }

        public static void ParseClassDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Class Declaration]");
            ParseDeclaration(m, depth + 1);
            ParseDeclarationNamespace(m, depth + 1);
        }

        public static void ParsePropertyDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Property Declaration]");
            ParseNamespaceItem(m, depth + 1);
            ParseDeclarationNamespace(m, depth + 1);
            Log(tabs + "\t[0x" + Utils.ReadU32(m).ToString("X8") + "]");
            Log(tabs + "\t[0x" + Utils.ReadU32(m).ToString("X8") + "]");
        }

        public static void ParseVariable(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Variable]");
            ParseNamespaceItem(m, depth + 1);
            ParseDeclarationUse(m, depth + 1);
            Log(tabs + $"\t[Array size: {Utils.ReadU32(m)}]");

        }

        public static void ParseDeclarationUse(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            byte b = (byte)m.ReadByte();
            Log(tabs + $"[DeclarationUse]");
            if ((EParseTreeElement)b == EParseTreeElement.TemplateInstance)
                ParseTemplateDeclarationUse(m, depth + 1);
            else
                Log(tabs + $"\t[{Utils.ReadString(m)}]");
        }

        public static void ParseTemplateDeclarationUse(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[TemplateDeclarationUse]");
            ParseNamespaceItem(m, depth + 1);
            byte count = (byte)m.ReadByte();
            for (int i = 0; i < count; i++)
                ParseDeclarationUse(m, depth + 1);
        }

        public static void ParseProtocolDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Protocol Declaration]");
            ParseDeclaration(m, depth + 1);
            ParseNameSpace(m, depth + 1);
        }

        public static void ParseRMC(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[RMC]");
            ParseProtocolDeclaration(m, depth + 1);
            ParseNameSpace(m, depth + 1);
        }

        public static void ParseParameter(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Parameter]");
            ParseVariable(m, depth + 1);
            ParseDeclarationUse(m, depth + 1);
            Log(tabs + $"\t[Array size: {Utils.ReadU32(m)}]");
            byte flags = (byte)m.ReadByte();
            string strFlags = "";
            switch (flags & 3)
            {
                case 1:
                    strFlags = "in";
                    break;
                case 2:
                    strFlags = "out";
                    break;
                case 3:
                    strFlags = "in/out";
                    break;
            }
            Log(tabs + $"\t[Flags: {strFlags}]");
        }

        public static void ParseReturnValue(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Return Value]");
            ParseVariable(m, depth + 1);
            ParseDeclarationUse(m, depth + 1);
            Log(tabs + "\t[0x" + Utils.ReadU32(m).ToString("X8") + "]");
        }

        public static void ParseTemplateDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Template Declaration]");
            ParseDeclaration(m, depth + 1);
            Log(tabs + "\t[0x" + Utils.ReadU32(m).ToString("X8") + "]");
        }

        public static void ParseSimpleTypeDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Simple Type Declaration]");
            ParseDeclaration(m, depth + 1);
        }

        public static void ParseDatasetDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Dataset Declaration]");
            ParseNamespaceItem(m, depth + 1);
            ParseDeclarationNamespace(m, depth + 1);
            ParseNameSpace(m, depth + 1);
        }

        public static void ParseDOClassDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[DO Class Declaration]");
            ParseNamespaceItem(m, depth + 1);
            ParseDeclarationNamespace(m, depth + 1);
            Log(tabs + "\t[" + Utils.ReadString(m) + "]");
            Log(tabs + "\t[0x" + Utils.ReadU32(m).ToString("X8") + "]");
            ParseNameSpace(m, depth + 1);
        }

        public static void ParseAdapterDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Adapter Declaration]");
            ParseDeclaration(m, depth + 1);
        }

        public static void ParseDupSpaceDeclaration(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Duplicated Space Declaration]");
            ParseDeclaration(m, depth + 1);
        }

        public static void ParseAction(Stream m, uint depth = 0)
        {
            string tabs = Utils.MakeTabs(depth);
            Log(tabs + "[Action]");
            ParseProtocolDeclaration(m, depth + 1);
            ParseNameSpace(m, depth + 1);
        }
    }
}
