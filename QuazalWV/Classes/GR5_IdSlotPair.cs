using System.IO;

namespace QuazalWV
{
    public class GR5_IdSlotPair
    {
        public uint Id { get; set; }
        public uint Slot { get; set; }
        public StoreService.VirtualCurrencyType VirtualCurrency { get; set; }
        
        public GR5_IdSlotPair()
        {

        }

        public GR5_IdSlotPair(Stream s)
        {
            Id = Helper.ReadU32(s);
            Slot = Helper.ReadU32(s);
            VirtualCurrency = (StoreService.VirtualCurrencyType)Helper.ReadU32(s);
        }

        public void ToBuffer(Stream s)
        {
            Helper.WriteU32(s, Id);
            Helper.WriteU32(s, Slot);
            Helper.WriteU32(s, (uint)VirtualCurrency);
        }
    }
}
