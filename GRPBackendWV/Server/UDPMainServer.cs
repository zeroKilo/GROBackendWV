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
    public static class UDPMainServer
    {
        public static readonly uint serverPID = 2;
        public static readonly object _sync = new object();
        public static bool _exit = false;
        public static ushort listenPort = 21031;
        public static UdpClient listener;

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
            WriteLog("Server started");
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
            WriteLog("Server stopped");
        }

        public static void ProcessPacket(byte[] data, IPEndPoint ep)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");
            WriteLog("received : " + sb.ToString(), !Global.useDetailedLog);
            QPacket p = new QPacket(data);
            WriteLog("received : " + p.ToStringDetailed(), !Global.useDetailedLog);
            WriteLog("received : " + p.ToStringShort(), Global.useDetailedLog);
            QPacket reply = null;
            ClientInfo client = null;
            if (p.type != QPacket.PACKETTYPE.SYN)
                client = Global.GetClientByIDrecv(p.m_uiSignature);
            switch (p.type)
            {
                case QPacket.PACKETTYPE.SYN:
                    reply = QPacketHandler.ProcessSYN(p, ep, out client);
                    break;
                case QPacket.PACKETTYPE.CONNECT:
                    if (client != null)
                        reply = QPacketHandler.ProcessCONNECT(client, p);
                    break;
                case QPacket.PACKETTYPE.DATA:
                    RMC.HandlePacket(listener, p);
                    break;
                case QPacket.PACKETTYPE.DISCONNECT:
                    if (client != null)
                        reply = QPacketHandler.ProcessDISCONNECT(client, p);
                    break;
                case QPacket.PACKETTYPE.PING:
                    if (client != null)
                        reply = QPacketHandler.ProcessPING(client, p);
                    break;
            }
            if (reply != null && client != null)
                Send(reply, client);
        }

        public static void Send(QPacket p, ClientInfo client)
        {
            byte[] data = p.toBuffer();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");
            WriteLog("send : " + sb.ToString(), !Global.useDetailedLog);
            WriteLog("send : " + p.ToStringDetailed(), !Global.useDetailedLog);
            WriteLog("send : " + p.ToStringShort(), Global.useDetailedLog);
            listener.Send(data, data.Length, client.ep);
        }

        private static void WriteLog(string s, bool toFileOnly = false)
        {
            Log.WriteLine("[UDP Main] " + s, toFileOnly);
        }
    }
}
