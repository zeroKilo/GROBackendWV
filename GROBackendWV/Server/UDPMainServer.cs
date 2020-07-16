using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using QuazalWV;

namespace GROBackendWV
{
    public static class UDPMainServer
    {
        public static readonly uint serverPID = 0x1000;
        public static readonly object _sync = new object();
        public static bool _exit = false;
        public static ushort listenPort = 21031;
        public static UdpClient listener;
        public static ushort _skipNextNAT = 0xFFFF;

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
                listener.Close();
        }

        public static void tMainThread(object obj)
        {
            WriteLog(1, "Server started");
            listener = new UdpClient(listenPort);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                lock (_sync)
                {
                    if (_exit)
                        break;
                }
                try
                {
                    byte[] bytes = listener.Receive(ref ep);
                    ProcessPacket(bytes, ep);
                }
                catch { }
            }
            WriteLog(1, "Server stopped");
        }

        public static void ProcessPacket(byte[] data, IPEndPoint ep)
        {
            QPacketHandler.ProcessPacket("UDP Backend", data, ep, listener, UDPMainServer.serverPID, UDPMainServer.listenPort);
        }

        private static void WriteLog(int priority, string s)
        {
            Log.WriteLine(priority, "[UDP Main] " + s);
        }
    }
}
