using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;

namespace GRPBackendWV
{
    public static class TCPServer
    {
        public static readonly object _sync = new object();
        public static bool _exit = false;
        private static TcpListener listener;
        private static string ip = "127.0.0.1";
        private static ushort listenPort = 80;
        private static ushort targetPort = 21030;

        public static void Start()
        {
            _exit = false;
            new Thread(tMainThread).Start();
        }

        public static void Stop()
        {
            lock (_sync)
            {
                _exit = true;
            }
            if (listener != null)
                listener.Stop();
        }

        public static void tMainThread(object obj)
        {
            listener = new TcpListener(IPAddress.Parse(ip), listenPort);
            listener.Start();
            Log.WriteLine("[TCP] Server started");
            while (true)
            {
                lock (_sync)
                {
                    if (_exit)
                        break;
                }
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Log.WriteLine("[TCP] Client connected");
                    new Thread(tClientHandler).Start(client);
                }
                catch { }
            }
            Log.WriteLine("[TCP] Server stopped");
        }

        public static void tClientHandler(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream ns = client.GetStream();
            MemoryStream m = new MemoryStream();
            //Read Content
            while (ns.DataAvailable)
                m.WriteByte((byte)ns.ReadByte());
            Log.WriteLine("[TCP] Received " + m.Length + " bytes");
            //Create Response
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            int count = 0;
            foreach (KeyValuePair<string, string> pair in responseData)
            {
                sb.Append("{\"Name\":\"" + pair.Key + "\",");
                sb.Append("\"Values\":[\"" + pair.Value.Replace("#IP#", ip).Replace("#PORT#", targetPort.ToString()) + "\"]}");
                if (count++ < 7)
                    sb.Append(",");
            }
            sb.Append("]");
            //Add HTTP header
            StringBuilder sb2 = new StringBuilder();
            AddHttpHeader(sb2, sb.Length);
            sb2.Append(sb.ToString());
            byte[] buff = Encoding.ASCII.GetBytes(sb2.ToString());
            //send and bye
            ns.Write(buff, 0, buff.Length);
            ns.Flush();
            ns.Close();
            Log.WriteLine("[TCP] Send " + buff.Length + " bytes");
        }

        private static void AddHttpHeader(StringBuilder sb, int contentlen)
        {
            sb.AppendLine("HTTP/1.1 200 OK");
            sb.AppendLine("Cache-Control: private");
            sb.AppendLine("Content-Length: " + contentlen);
            sb.AppendLine("Content-Type: application/json; charset=utf-8");
            sb.AppendLine("Server: Microsoft-IIS/7.5");
            sb.AppendLine("X-AspNet-Version: 2.0.50727");
            sb.AppendLine("X-Powered-By: ASP.NET");
            sb.AppendLine("Date: Fri, 01 Nov 2019 14:04:13 GMT");
            sb.AppendLine("");
        }

        private static Dictionary<string, string> responseData = new Dictionary<string, string>()
        {
            {"SandboxUrl", @"prudp:\/address=#IP#;port=#PORT#"},
            {"SandboxUrlWS", @"#IP#:#PORT#"},
            {"uplay_DownloadServiceUrl", @"#IP#\/UplayServices\/UplayFacade\/DownloadServicesRESTXML.svc\/REST\/XML\/?url="},
            {"uplay_DynContentBaseUrl", @"#IP#\/u\/Uplay\/"},
            {"uplay_DynContentSecureBaseUrl", @"#IP#\/"},
            {"uplay_LinkappBaseUrl", @"#IP#\/u\/Uplay\/Packages\/linkapp\/1.1\/"},
            {"uplay_PackageBaseUrl", @"#IP#\/u\/Uplay\/Packages\/1.0.1\/"},
            {"uplay_WebServiceBaseUrl", @"#IP#\/UplayServices\/UplayFacade\/ProfileServicesFacadeRESTXML.svc\/REST\/"},
        };
    }
}
