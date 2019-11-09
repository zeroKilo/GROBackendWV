using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketRequestLoginCustomData : RMCPacketHeader
    {
        public string user;
        public string className;
        public string username;
        public string onlineKey;
        public string password;

        public RMCPacketRequestLoginCustomData()
        { 
        }

        public RMCPacketRequestLoginCustomData(Stream s)
        {
            user = Helper.ReadString(s);
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[LoginCustomData Request : user=" + user + " className=" + className + "]");
            sb.AppendLine("\t\t[Username   : " + username + "]");
            sb.AppendLine("\t\t[Online Key : " + onlineKey + "]");
            sb.AppendLine("\t\t[Password   : " + password + "]");
            return sb.ToString();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream result = new MemoryStream();
            Helper.WriteString(result, user);
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
    }
}
