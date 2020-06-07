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

        public void toBuffer(Stream s)
        {
            Helper.WriteString(s, sSessionDescription);
            Helper.WriteString(s, string2);
            Helper.WriteString(s, sSessionDiscovery);
        }

        public string getDesc()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[SharedSessionDescription]");
            sb.AppendLine(" Session Description = " + sSessionDescription);
            sb.AppendLine(" String 2 = " + string2);
            sb.AppendLine(" Session Discovery = " + sSessionDiscovery);
            return sb.ToString();
        }
    }
}
