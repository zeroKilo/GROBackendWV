﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class QPacketHandler
    {
        public static QPacket ProcessSYN(QPacket p, IPEndPoint ep, out ClientInfo client)
        {
            client = Global.GetClientByEndPoint(ep);
            if (client == null)
            {
                Log.WriteLine(2, "[QUAZAL] Creating new client data...");
                client = new ClientInfo();
                client.ep = ep;
                client.IDrecv = Global.idCounter++;
                client.PID = Global.pidCounter++;
                Global.clients.Add(client);
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
            return reply;
        }

        public static QPacket ProcessCONNECT(ClientInfo client, QPacket p)
        {
            client.IDsend = p.m_uiConnectionSignature;
            QPacket reply = new QPacket();
            reply.m_oSourceVPort = p.m_oDestinationVPort;
            reply.m_oDestinationVPort = p.m_oSourceVPort;
            reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            reply.type = QPacket.PACKETTYPE.CONNECT;
            reply.m_bySessionID = p.m_bySessionID;
            reply.m_uiSignature = client.IDsend;
            reply.uiSeqId = p.uiSeqId;
            reply.m_uiConnectionSignature = client.IDrecv;
            if (p.payload != null && p.payload.Length > 0)
                reply.payload = MakeConnectPayload(client,p);
            else
                reply.payload = new byte[0];
            return reply;
        }

        private static byte[] MakeConnectPayload(ClientInfo client, QPacket p)
        {
            MemoryStream m = new MemoryStream(p.payload);
            uint size = Helper.ReadU32(m);
            byte[] buff = new byte[size];
            m.Read(buff, 0, (int)size);
            size = Helper.ReadU32(m) - 16;
            buff = new byte[size];
            m.Read(buff, 0, (int)size);
            buff = Helper.Decrypt(client.sessionKey, buff);
            m = new MemoryStream(buff);
            uint pid = Helper.ReadU32(m); //user pid
            uint cid = Helper.ReadU32(m); //connection id
            uint responseCode = Helper.ReadU32(m);
            Log.WriteLine(1, $"[UDP Secure] CONNECT: PID: 0x{pid:X8}, CID: {cid}, response code 0x{responseCode:X8}");
            m = new MemoryStream();
            Helper.WriteU32(m, 4);
            Helper.WriteU32(m, responseCode + 1);
            return m.ToArray();
        }

        public static QPacket ProcessDISCONNECT(ClientInfo client, QPacket p)
        {
            QPacket reply = new QPacket();
            reply.m_oSourceVPort = p.m_oDestinationVPort;
            reply.m_oDestinationVPort = p.m_oSourceVPort;
            reply.flags = new List<QPacket.PACKETFLAG>() { QPacket.PACKETFLAG.FLAG_ACK };
            reply.type = QPacket.PACKETTYPE.DISCONNECT;
            reply.m_bySessionID = p.m_bySessionID;
            reply.m_uiSignature = client.IDsend - 0x10000;
            reply.uiSeqId = p.uiSeqId;
            reply.payload = new byte[0];
            return reply;
        }

        public static QPacket ProcessPING(ClientInfo client, QPacket p)
        {
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
            return reply;
        }


        public static List<ulong> timeToIgnore = new List<ulong>();

        public static void ProcessPacket(string source, byte[] data, IPEndPoint ep, UdpClient listener, uint serverPID, ushort listenPort, bool removeConnectPayload = false)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");
            while (true)
            {
                QPacket p = new QPacket(data);
                MemoryStream m = new MemoryStream(data);
                byte[] buff = new byte[(int)p.realSize];
                m.Seek(0, 0);
                m.Read(buff, 0, buff.Length);
                Log.LogPacket(false, buff);
                Log.WriteLine(5, "[" + source + "] received : " + p.ToStringShort());
                Log.WriteLine(10, "[" + source + "] received : " + sb.ToString());
                Log.WriteLine(10, "[" + source + "] received : " + p.ToStringDetailed());
                QPacket reply = null;
                ClientInfo client = null;
                if (p.type != QPacket.PACKETTYPE.SYN && p.type != QPacket.PACKETTYPE.NATPING)
                    client = Global.GetClientByIDrecv(p.m_uiSignature);
                switch (p.type)
                {
                    case QPacket.PACKETTYPE.SYN:
                        reply = QPacketHandler.ProcessSYN(p, ep, out client);
                        break;
                    case QPacket.PACKETTYPE.CONNECT:
                        if (client != null && !p.flags.Contains(QPacket.PACKETFLAG.FLAG_ACK))
                        {
                            client.sPID = serverPID;
                            client.sPort = listenPort;
                            if (removeConnectPayload)
                            {
                                p.payload = new byte[0];
                                p.payloadSize = 0;
                            }
                            reply = QPacketHandler.ProcessCONNECT(client, p);
                        }
                        break;
                    case QPacket.PACKETTYPE.DATA:
                        if (p.m_oSourceVPort.type == QPacket.STREAMTYPE.OldRVSec)
                            RMC.HandlePacket(listener, p);
                        if (p.m_oSourceVPort.type == QPacket.STREAMTYPE.DO)
                            DO.HandlePacket(listener, p);
                        break;
                    case QPacket.PACKETTYPE.DISCONNECT:
                        if (client != null)
                            reply = QPacketHandler.ProcessDISCONNECT(client, p);
                        break;
                    case QPacket.PACKETTYPE.PING:
                        if (client != null)
                            reply = QPacketHandler.ProcessPING(client, p);
                        break;
                    case QPacket.PACKETTYPE.NATPING:
                        ulong time = BitConverter.ToUInt64(p.payload, 5);
                        if (timeToIgnore.Contains(time))
                            timeToIgnore.Remove(time);
                        else
                        {
                            reply = p;
                            m = new MemoryStream();
                            byte b = (byte)(reply.payload[0] == 1 ? 0 : 1);
                            m.WriteByte(b);
                            Helper.WriteU32(m, 0x1234); //RVCID
                            Helper.WriteU64(m, time);
                            reply.payload = m.ToArray();
                            Send(source, reply, ep, listener);
                            m = new MemoryStream();
                            b = (byte)(b == 1 ? 0 : 1);
                            m.WriteByte(b);
                            Helper.WriteU32(m, 0x1234); //RVCID
                            time = Helper.MakeTimestamp();
                            timeToIgnore.Add(time);
                            Helper.WriteU64(m, Helper.MakeTimestamp());
                            reply.payload = m.ToArray();
                        }
                        break;
                }
                if (reply != null)
                    Send(source, reply, ep, listener);
                if (p.realSize != data.Length)
                {
                    m = new MemoryStream(data);
                    int left = (int)(data.Length - p.realSize);
                    byte[] newData = new byte[left];
                    m.Seek(p.realSize, 0);
                    m.Read(newData, 0, left);
                    data = newData;
                }
                else
                    break;
            }
        }

        public static void Send(string source, QPacket p, IPEndPoint ep, UdpClient listener)
        {
            byte[] data = p.toBuffer();
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("X2") + " ");
            Log.WriteLine(5, "[" + source + "] send : " + p.ToStringShort());
            Log.WriteLine(10, "[" + source + "] send : " + sb.ToString());
            Log.WriteLine(10, "[" + source + "] send : " + p.ToStringDetailed());
            listener.Send(data, data.Length, ep);
            Log.LogPacket(true, data);
        }
    }
}
