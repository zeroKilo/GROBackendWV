using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseStoreService_CompleteBuyWeaponAndAttachComponents : RMCPResponse
    {
        public List<GR5_UserItem> Inventory { get; set; }
        public List<Map_U32_VectorU32> UserComponentLists { get; set; }

        public RMCPacketResponseStoreService_CompleteBuyWeaponAndAttachComponents()
        {
            Inventory = new List<GR5_UserItem>();
            UserComponentLists = new List<Map_U32_VectorU32>();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            
            Helper.WriteU32(m, (uint)Inventory.Count);
            foreach(GR5_UserItem item in Inventory)
                item.toBuffer(m);

            Helper.WriteU32(m, (uint)UserComponentLists.Count);
            foreach (Map_U32_VectorU32 userCompList in UserComponentLists)
                userCompList.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[CompleteBuyWeaponAndAttachComponents Response]";
        }

        public override string PayloadToString()
        {
            return $"\t[Transaction ID: {Inventory}]";
        }
    }
}
