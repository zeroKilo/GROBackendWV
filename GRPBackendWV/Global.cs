using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public static class Global
    {
        public static readonly string keyDATA = "CD&ML";
        public static readonly string keyCheckSum = "8dtRv2oj";
        public static string serverBindAddress = "127.0.0.1";
        public static uint idCounter = 0x12345678;
        public static uint pidCounter = 0x1234;
        public static string sessionURL = "prudp:/address=127.0.0.1;port=21032;sid=1;stream=3;type=2 public";
            //"prudps:/address=127.0.0.1;port=21032;CID=1;PID=4096;sid=1;stream=3;type=2";
        public static List<ClientInfo> clients = new List<ClientInfo>();

        public static ClientInfo GetClientByEndPoint(IPEndPoint ep)
        {
            foreach (ClientInfo c in clients)
                if (c.ep.Address.ToString() == ep.Address.ToString() && c.ep.Port == ep.Port)
                    return c;
            WriteLog(1, "Error : Cant find client for end point : " + ep.ToString());
            return null;
        }

        public static ClientInfo GetClientByIDsend(uint id)
        {
            foreach (ClientInfo c in clients)
                if (c.IDsend == id)
                    return c;
            WriteLog(1, "Error : Cant find client for id : 0x" + id.ToString("X8"));
            return null;
        }

        public static ClientInfo GetClientByIDrecv(uint id)
        {
            foreach (ClientInfo c in clients)
                if (c.IDrecv == id)
                    return c;
            WriteLog(1, "Error : Cant find client for id : 0x" + id.ToString("X8"));
            return null;
        }

        private static void WriteLog(int priority, string s)
        {
            Log.WriteLine(priority, "[Global] " + s);
        }
    }
}
