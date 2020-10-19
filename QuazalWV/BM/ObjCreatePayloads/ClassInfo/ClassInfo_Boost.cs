using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class ClassInfo_Boost
    {
        const byte memBufferSize = 13;
        bool bActive;
        uint inventoryId;
        uint itemId;
        float value;

        public ClassInfo_Boost()
        {
            bActive = true;
            inventoryId = 0x7777;
            itemId = 0x1111;
            value = 1f;
        }
        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, memBufferSize);
            Helper.WriteBool(m, bActive);
            Helper.WriteU32(m, inventoryId);
            Helper.WriteU32(m, itemId);
            Helper.WriteFloat(m, value);
            return m.ToArray();
        }
    }
}
