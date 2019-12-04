using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseUnlockService_Method3 : RMCPacketReply
    {
        public class UserUnlockResult
        {
            public uint unlockID;
            public byte result;
            public void ToBuffer(Stream stream)
            {
                Helper.WriteU32(stream, unlockID);
                Helper.WriteU8(stream, result);
            }
        }
        public List<UserUnlockResult> results = new List<UserUnlockResult>();
        public override byte[] ToBuffer()
        {
            using (var mem = new MemoryStream())
            {
                Helper.WriteU32(mem, (uint)results.Count);
                foreach (var result in results)
                    result.ToBuffer(mem);
                return mem.ToArray();
            }
        }

        public override string ToString()
        {
            return "[RMCPacketResponseUnlockService_Method3]";
        }
    }
}
