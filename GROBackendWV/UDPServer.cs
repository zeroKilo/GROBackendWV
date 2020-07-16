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
    public static class UDPServer
    {
        public static readonly object _sync = new object();
        public static bool _exit = false;
        private static UdpClient listener;
        private static ushort listenPort = 21030;
        private static Random rnd = new Random();
        private static List<ClientInfo> clients = new List<ClientInfo>();
        private static uint idCounter = 0x12345678;

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
            Log.WriteLine("[UDP] Server started");
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
            Log.WriteLine("[UDP] Server stopped");
        }

        public static void ProcessPacket(byte[] data, IPEndPoint ep)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");
            Log.WriteLine("[UDP] received : " + sb.ToString());
            QPacket p = new QPacket(data);
            Log.WriteLine("[UDP] received : " + p);
            switch (p.type)
            {
                case QPacket.PACKETTYPE.SYN:
                    ProcessSYN(p, ep);
                    break;
                case QPacket.PACKETTYPE.CONNECT:
                    ProcessCONNECT(p);
                    break;
                case QPacket.PACKETTYPE.DATA:
                    ProcessDATA(p);
                    break;
                case QPacket.PACKETTYPE.DISCONNECT:
                    ProcessDISCONNECT(p);
                    break;
                case QPacket.PACKETTYPE.PING:
                    ProcessPING(p);
                    break;
            }
        }

        public static ClientInfo GetClientByEndPoint(IPEndPoint ep)
        {
            foreach (ClientInfo c in clients)
                if (c.ep.Address.ToString() == ep.Address.ToString() && c.ep.Port == ep.Port)
                    return c;
            return null;
        }

        public static ClientInfo GetClientByIDsend(uint id)
        {
            foreach (ClientInfo c in clients)
                if (c.IDsend == id)
                    return c;
            return null;
        }

        public static ClientInfo GetClientByIDrecv(uint id)
        {
            foreach (ClientInfo c in clients)
                if (c.IDrecv == id)
                    return c;
            return null;
        }

        public static void Send(QPacket p, ClientInfo client)
        {
            byte[] data = p.toBuffer();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");
            Log.WriteLine("[UDP] send : " + sb.ToString());
            Log.WriteLine("[UDP] send : " + p);
            listener.Send(data, data.Length, client.ep);
        }

        public static void ProcessSYN(QPacket p, IPEndPoint ep)
        {
            ClientInfo client = GetClientByEndPoint(ep);
            if (client == null)
            {
                client = new ClientInfo();
                client.ep = ep;
                client.IDrecv = idCounter++;
                clients.Add(client);
            }
            QPacket reply = new QPacket();
            reply.m_oSourceVPort = p.m_oDestinationVPort;
            reply.m_oDestinationVPort = p.m_oSourceVPort;
            reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            reply.type = QPacket.PACKETTYPE.SYN;
            reply.m_bySessionID = p.m_bySessionID;
            reply.m_uiSignature = p.m_uiSignature;
            reply.uiSeqId = p.uiSeqId;
            reply.m_uiConnectionSignature = client.IDrecv;
            reply.payload = new byte[0];
            Send(reply, client);
        }

        public static void ProcessCONNECT(QPacket p)
        {
            ClientInfo client = GetClientByIDrecv(p.m_uiSignature);
            if (client == null)
            {
                Log.WriteLine("[UDP] Cand find client for id : 0x" + p.m_uiSignature.ToString("X8"));
                return;
            }
            client.IDsend = p.m_uiConnectionSignature;
            QPacket reply = new QPacket();
            if (p.payload != null && p.payload.Length > 1)
            {
                using (var mem = new MemoryStream(p.payload))
                {
                    var ticketLen = Helper.ReadU32(mem);
                    var ticket = new byte[ticketLen];
                    for (int i = 0; i < ticketLen; i++)
                        ticket[i] = Helper.ReadU8(mem);
                    var requestDataLen = Helper.ReadU32(mem);
                    var requestData = new byte[requestDataLen];
                    for (int i = 0; i < requestDataLen; i++)
                        requestData[i] = Helper.ReadU8(mem);
                    var sessionKey = new byte[] { 0x9C, 0xB0, 0x1D, 0x7A, 0x2C, 0x5A, 0x6C, 0x5B, 0xED, 0x12, 0x68, 0x45, 0x69, 0xAE, 0x09, 0x0D };
                    var decryptedRequestData = Helper.Decrypt(sessionKey, requestData);
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in decryptedRequestData)
                        sb.Append(b.ToString("X2"));
                    uint rcv;
                    using(var d = new MemoryStream(decryptedRequestData))
                    {
                        var uPid = Helper.ReadU32(d);
                        var cid = Helper.ReadU32(d);
                        rcv = Helper.ReadU32(d);
                    }
                    using (var r = new MemoryStream())
                    {
                        reply.m_oSourceVPort = p.m_oDestinationVPort;
                        reply.m_oDestinationVPort = p.m_oSourceVPort;
                        reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK, QPacket.PACKETFLAG.FLAG_HAS_SIZE };
                        reply.type = QPacket.PACKETTYPE.CONNECT;
                        reply.m_bySessionID = p.m_bySessionID;
                        reply.m_uiSignature = client.IDsend;
                        reply.uiSeqId = p.uiSeqId;
                        reply.m_uiConnectionSignature = client.IDrecv;
                        Helper.WriteU32(r, 4);
                        Helper.WriteU32(r, rcv + 1);
                        reply.payload = r.ToArray();
                    }
                }
            }
            else
            {
                reply.m_oSourceVPort = p.m_oDestinationVPort;
                reply.m_oDestinationVPort = p.m_oSourceVPort;
                reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
                reply.type = QPacket.PACKETTYPE.CONNECT;
                reply.m_bySessionID = p.m_bySessionID;
                reply.m_uiSignature = client.IDsend;
                reply.uiSeqId = p.uiSeqId;
                reply.m_uiConnectionSignature = client.IDrecv;
                reply.payload = new byte[0];
            }
            Send(reply, client);

        }

        public static void ProcessDATA(QPacket p)
        {
            RMC.HandlePacket(p);
        }

        public static void ProcessDISCONNECT(QPacket p)
        {
            ClientInfo client = GetClientByIDrecv(p.m_uiSignature);
            if (client == null)
            {
                Log.WriteLine("[UDP] Cand find client for id : 0x" + p.m_uiSignature.ToString("X8"));
                return;
            }
            QPacket reply = new QPacket();
            reply.m_oSourceVPort = p.m_oDestinationVPort;
            reply.m_oDestinationVPort = p.m_oSourceVPort;
            reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            reply.type = QPacket.PACKETTYPE.DISCONNECT;
            reply.m_bySessionID = p.m_bySessionID;
            reply.m_uiSignature = client.IDsend;
            reply.uiSeqId = p.uiSeqId;
            reply.m_uiConnectionSignature = client.IDrecv;
            reply.payload = new byte[0];
            Send(reply, client);
        }

        public static void ProcessPING(QPacket p)
        {
            ClientInfo client = GetClientByIDrecv(p.m_uiSignature);
            if (client == null)
            {
                Log.WriteLine("[UDP] Cand find client for id : 0x" + p.m_uiSignature.ToString("X8"));
                return;
            }
            QPacket reply = new QPacket();
            reply.m_oSourceVPort = p.m_oDestinationVPort;
            reply.m_oDestinationVPort = p.m_oSourceVPort;
            reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            reply.type = QPacket.PACKETTYPE.PING;
            reply.m_bySessionID = p.m_bySessionID;
            reply.m_uiSignature = client.IDsend;
            reply.uiSeqId = p.uiSeqId;
            reply.m_uiConnectionSignature = client.IDrecv;
            reply.payload = new byte[0];
            Send(reply, client);
        }
    }
}
