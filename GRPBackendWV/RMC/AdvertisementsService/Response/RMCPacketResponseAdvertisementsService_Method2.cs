using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAdvertisementsService_Method2 : RMCPacketReply
    {

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();            
            //return m.ToArray();
            return new byte[16];
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAdvertisementsService_Method2]";
        }
    }
}
