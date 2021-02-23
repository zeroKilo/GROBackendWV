using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuazalWV
{
    public class GR5_NewsMessage
    {
        public GR5_NewsHeader header;
        //body is xml data, see wiki for format info
        public string m_body =
        new XElement("news",
        //it might be possible that you can have multiple messages here
        //i guess that's why there are all the range validations etc. in rdv.dll
            new XElement("message",
                //core attributes
                new XAttribute("unkattr", "somevalue"),
                new XAttribute("unkattrii", "somevalueii"),
                new XAttribute("type", 73498),
                new XAttribute("icon", 11),
                new XAttribute("oasis", 73498),
                new XAttribute("pid", 4660),
                new XAttribute("time", (uint)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds)//TODO: time format isnt valid
                //optional message-specific attributes
                )
            ).ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "");
                
        public void toBuffer(Stream s)
        {
            header.toBuffer(s);
            Helper.WriteString(s, m_body);
        }
    }
}
