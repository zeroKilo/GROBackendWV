using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DupCreateMasterStation
    {
        public static byte[] CreatePayload()
        {
            MemoryStream m = new MemoryStream(); 
            m.WriteByte(1);
            new DS_ConnectionInfo().toBuffer(m);
            m.WriteByte(1);
            new StationIdentification().toBuffer(m);
            m.WriteByte(1);
            new StationInfo().toBuffer(m);
            m.WriteByte(1);
            Helper.WriteU16(m, 3); //StationState
            return m.ToArray();
        }
    }
}
