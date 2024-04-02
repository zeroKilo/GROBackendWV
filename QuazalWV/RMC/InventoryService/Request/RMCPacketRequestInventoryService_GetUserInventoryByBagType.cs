using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestInventoryService_GetUserInventoryByBagType : RMCPRequest
    {
        public uint pid;
        public uint bagCount;
        public List<uint> requestedBagTypes = new List<uint>();

        public RMCPacketRequestInventoryService_GetUserInventoryByBagType(Stream s)
        {
            pid = Helper.ReadU32(s);
            bagCount = Helper.ReadU32(s);
            for (int i = 0; i < bagCount; i++)
                requestedBagTypes.Add(Helper.ReadU32(s));
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[Number of requested bags : " + bagCount + "]");
            return "";
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[GetUserInventoryByBagType Request: nbBags=" + bagCount + "]";
        }
    }
}
