using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class ClientInfo
    {
        public uint PID;
        public uint sPID;
        public ushort sPort;
        public uint IDrecv;
        public uint IDsend;
        public byte sessionID;
        public byte[] sessionKey;
        public ushort seqCounter;
        public ushort seqCounterDO;
        public ushort callCounterDO_RMC;
        public uint callCounterRMC;
        public uint stationID;
        public string name;
        public string pass;
        public IPEndPoint ep;
        public UdpClient udp;
        public bool bootStrapDone = false;
        public bool matchStartSent = false;
        public bool playerCreateStuffSent1 = false;
        public bool playerCreateStuffSent2 = false;
        public byte netRulesState = 3;
        public byte playerAbstractState = 2;
        public Payload_PlayerParameter settings = new Payload_PlayerParameter(new byte[0x40]);
    }
}
