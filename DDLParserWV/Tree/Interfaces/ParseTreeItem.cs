using System.IO;
using System.Text;

namespace DDLParserWV
{
    public abstract class ParseTreeItem<T> : ParseTreeItemBase where T : ParseTreeItem<T>
    {
        protected abstract T ParseTyped(Stream s, StringBuilder log, uint depth);
        public override ParseTreeItemBase Parse(Stream s, StringBuilder log, uint depth)
        {
            return ParseTyped(s, log, depth);
        }
    }
}
