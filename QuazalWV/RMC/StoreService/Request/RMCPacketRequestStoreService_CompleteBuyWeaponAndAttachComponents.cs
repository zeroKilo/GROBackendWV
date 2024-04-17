using QuazalWV.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestStoreService_CompleteBuyWeaponAndAttachComponents : RMCPRequest
    {
        public uint TransactionId { get; set; }

        public RMCPacketRequestStoreService_CompleteBuyWeaponAndAttachComponents(Stream s)
        {
            TransactionId = Helper.ReadU32(s);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, TransactionId);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[CompleteBuyWeaponAndAttachComponents Request]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Transaction ID: {TransactionId}]");
            return sb.ToString();
        }
    }
}