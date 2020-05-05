using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_NewsMessage
    {
        public GR5_NewsHeader header;
        public string m_body;
        public void toBuffer(Stream s)
        {
            header.toBuffer(s);
            Helper.WriteString(s, m_body);
        }
    }
}
