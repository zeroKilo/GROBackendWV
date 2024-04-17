using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV.Classes
{
    public class GR5_CartItem
    {
        public uint SkuId { get; set; }
        public uint NumberToBuy { get; set; }
        public StoreService.VirtualCurrencyType VirtualCurrencyType { get; set; }

        public GR5_CartItem()
        {
        
        }

        public GR5_CartItem(Stream s)
        {
            SkuId = Helper.ReadU32(s);
            NumberToBuy = Helper.ReadU32(s);
            VirtualCurrencyType = (StoreService.VirtualCurrencyType)Helper.ReadU32(s);
        }

        public void ToBuffer(Stream s)
        {
            Helper.WriteU32(s, SkuId);
            Helper.WriteU32(s, NumberToBuy);
            Helper.WriteU32(s, (uint)VirtualCurrencyType);
        }
    }
}
