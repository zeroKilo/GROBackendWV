using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseRequestTicket : RMCPResponse
    {
        public uint resultCode = 0x10001;
        public byte[] ticketBuffer;
        public KerberosTicket ticket;

        public RMCPacketResponseRequestTicket(uint pid, uint sPID)
        {
            ticket = new KerberosTicket(sPID);
            ticket.userPID = pid;
            ticket.sessionKey = new byte[] { 0x9C, 0xB0, 0x1D, 0x7A, 0x2C, 0x5A, 0x6C, 0x5B, 0xED, 0x12, 0x68, 0x45, 0x69, 0xAE, 0x09, 0x0D };
            ticket.ticket = new byte[] { 0x76, 0x21, 0x4B, 0xA6, 0x21, 0x96, 0xD3, 0xF3, 0x9A, 0x8C, 0x7A, 0x27, 0x0D, 0xD9, 0xB3, 0xFA, 0x21, 0x0E, 0xED, 0xAF, 0x42, 0x63, 0x92, 0x95, 0xC1, 0x16, 0x54, 0x08, 0xEE, 0x6E, 0x69, 0x17, 0x35, 0x78, 0x2E, 0x6E };
            ticketBuffer = ticket.toBuffer();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, resultCode);
            Helper.WriteU32(m, (uint)ticketBuffer.Length);
            foreach (byte b in ticketBuffer)
                Helper.WriteU8(m, b);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RequestTicket Response]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[Result Code   : 0x" + resultCode.ToString("X8") + "]");
            sb.Append("\t[Ticket Buffer : { ");
            foreach (byte b in ticketBuffer)
                sb.Append(b.ToString("X2") + " ");
            sb.AppendLine("}]");
            return sb.ToString();
        }
    }
}
