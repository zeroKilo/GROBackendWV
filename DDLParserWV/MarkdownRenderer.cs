namespace DDLParserWV
{
    public static class MarkdownRenderer
    {
        public static string RenderProtocol(ProtocolDeclaration protocol)
        {
            string output = RenderHeader(protocol);
            output += "\n";
            output += RenderMethods(protocol);
            return output;
        }

        public static string RenderHeader(ProtocolDeclaration protocol)
        {
            string output = $"# {protocol.Declaration.NsItem.TreeItemName}\n";
            output += @"
| Method ID | Method Name |
|-----------|-------------|
";
            uint idx = 1;
            foreach(var item in protocol.NameSpace.Items)
            {
                if (item.Type == EParseTreeElement.RMC)
                {
                    output += RenderMethodTableLine((RMC)item, idx);
                    idx++;
                }
            }
            return output;
        }

        public static string RenderMethodTableLine(RMC method, uint index)
        {
            string name = method.GetName();
            return $"| {index} | [{name}](#{index}-{name.ToLower()}) |\n";
        }

        public static string RenderMethods(ProtocolDeclaration protocol)
        {
            string output = "";
            uint idx = 1;
            foreach (var item in protocol.NameSpace.Items)
            {
                if (item.Type == EParseTreeElement.RMC)
                {
                    output += RenderMethodDefinition((RMC)item, idx);
                    idx++;
                }
            }
            return output;
        }

        public static string RenderMethodDefinition(RMC method, uint index)
        {
            string name = method.GetName();
            string output = $"# ({index}) {name}\n\n";
            output += RenderRequestDefinition(method);
            output += "\n";
            output += RenderResponseDefinition(method);
            output += "\n";
            return output;
        }

        public static string RenderRequestDefinition(RMC method)
        {
            string output = "## Request\n";
            uint reqParams = 0;
            foreach (var item in method.NameSpace.Items)
            {
                if (item.Type == EParseTreeElement.Parameter)
                {
                    var param = (Parameter)item;
                    if (param.IsRequest())
                        reqParams++;
                }
            }

            if (reqParams == 0)
            {
                output += "This method does not take any parameters.\n";
                return output;
            }

            output += @"
| Type | Name |
|------|------|
";
            foreach (var item in method.NameSpace.Items)
            {
                if (item.Type == EParseTreeElement.Parameter)
                {
                    var param = (Parameter)item;
                    if (param.IsRequest())
                        output += RenderParameter(param);
                }
            }
            return output;
        }

        public static string RenderResponseDefinition(RMC method)
        {
            string output = "## Response\n";
            uint resParams = 0;
            foreach (var item in method.NameSpace.Items)
            {
                if (item.Type == EParseTreeElement.ReturnValue)
                    resParams++;
                else if (item.Type == EParseTreeElement.Parameter)
                {
                    var param = (Parameter)item;
                    if (param.IsResponse())
                        resParams++;
                }
            }

            if (resParams == 0)
            {
                output += "This method does not return anything.\n";
                return output;
            }

            output += @"
| Type | Name |
|------|------|
";
            foreach (var item in method.NameSpace.Items)
            {
                if (item.Type == EParseTreeElement.ReturnValue)
                    output += RenderReturnValue((ReturnValue)item);
                else if (item.Type == EParseTreeElement.Parameter)
                {
                    var param = (Parameter)item;
                    if (param.IsResponse())
                        output += RenderParameter(param);
                }
            }
            return output;
        }

        public static string RenderParameter(Parameter param)
        {
            return $"| {param.GetFullType()} | {param.GetName()} |\n";
        }

        public static string RenderReturnValue(ReturnValue retVal)
        {
            return $"| {retVal.GetFullType()} | {retVal.GetName()} |\n";
        }
    }
}
