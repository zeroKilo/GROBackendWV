using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestRegisterEx : RMCPRequest
    {
        public List<string> stationUrls;
        public string className;
        public string username;
        public string onlineKey;
        public string password;

        public RMCPacketRequestRegisterEx(Stream s)
        {
            stationUrls = Helper.ReadStringList(s);
            className = Helper.ReadString(s);
            ProcessData(s);
        }

        private void ProcessData(Stream s)
        {
            Helper.ReadU32(s);
            Helper.ReadU32(s);
            switch (className)
            {
                case "UbiAuthenticationLoginCustomData":
                    username = Helper.ReadString(s);
                    onlineKey = Helper.ReadString(s);
                    password = Helper.ReadString(s);
                    break;
            }
        }

        public override byte[] ToBuffer()
        {
            MemoryStream result = new MemoryStream();
            Helper.WriteStringList(result, stationUrls);
            Helper.WriteString(result, className);
            MemoryStream m = new MemoryStream();
            Helper.WriteString(m, username);
            Helper.WriteString(m, onlineKey);
            Helper.WriteString(m, password);
            byte[] buff = m.ToArray();
            Helper.WriteU32(result, (uint)(buff.Length + 4));
            Helper.WriteU32(result, (uint)buff.Length);
            result.Write(buff, 0, buff.Length);
            return result.ToArray();
        }

        public override string ToString()
        {
            return "[RegisterEx Request : className=" + className + "]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[Station List :]");
            foreach (string s in stationUrls)
                sb.AppendLine("\t\t\t[\"" + s + "\"]");
            sb.AppendLine("\t[Username     : " + username + "]");
            sb.AppendLine("\t[Online Key   : " + onlineKey + "]");
            sb.AppendLine("\t[Password     : " + password + "]");
            return sb.ToString();
        }
    }
}