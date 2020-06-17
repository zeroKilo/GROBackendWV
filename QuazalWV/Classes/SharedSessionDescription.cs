using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class SharedSessionDescription
    {
        public string sSessionDescription = "";
        public string string2 = "";
        public string sSessionDiscovery = "";

        public SharedSessionDescription() { }
        public SharedSessionDescription(Stream s)
        {
            sSessionDescription = Helper.ReadString(s);
            string2 = Helper.ReadString(s);
            sSessionDiscovery = Helper.ReadString(s);
        }

        public void toBuffer(Stream s)
        {
            Helper.WriteString(s, sSessionDescription);
            Helper.WriteString(s, string2);
            Helper.WriteString(s, sSessionDiscovery);
        }

        public string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "[SharedSessionDescription]");
            sb.AppendLine(tabs + " Session Description = " + sSessionDescription);
            sb.AppendLine(tabs + " String 2            = " + string2);
            sb.AppendLine(tabs + " Session Discovery   = " + sSessionDiscovery);
            return sb.ToString();
        }
    }
}
