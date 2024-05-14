using System.IO;
using System.Text;

namespace QuazalWV
{
    public class RMCPacketRequestStoreService_CompleteBuyAbilityWithUpgrades : RMCPRequest
    {
        public uint TransactionId { get; set; }

        public RMCPacketRequestStoreService_CompleteBuyAbilityWithUpgrades(Stream s)
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
            return "[CompleteBuyAbilityWithUpgrades Request]";
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Transaction ID: {TransactionId}]");
            return sb.ToString();
        }
    }
}
