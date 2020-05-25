using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class Payload_SyncResponse
    {
        public byte[] Create()
        {
            MemoryStream m = new MemoryStream();
            return m.ToArray();
        }
    }
}
