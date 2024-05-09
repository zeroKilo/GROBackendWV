using System.IO;
using System.Text;

namespace QuazalWV
{
    public class RMCPacketRequestStoreService_CompleteBuyArmourAndAttachInserts : RMCPRequest
    {
        public uint TransactionId { get; set; }

        public RMCPacketRequestStoreService_CompleteBuyArmourAndAttachInserts(Stream s)
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
            return "[CompleteBuyArmourAndAttachInserts Request]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Transaction ID: {TransactionId}]");
            return sb.ToString();
        }
    }
}