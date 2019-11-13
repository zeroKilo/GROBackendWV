using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseUnlockService_GetCurrentUserUnlock : RMCPacketReply
    {
        public class Unlock
        {
            public uint[] unk1 = new uint[2];
            public byte unk2;
            public uint[] unk3 = new uint[13];

            public void toBuffer(Stream s)
            {
                foreach (uint u in unk1)
                    Helper.WriteU32(s, u);
                Helper.WriteU8(s, unk2);
                foreach (uint u in unk3)
                    Helper.WriteU32(s, u);
            }
        }

        List<Unlock> unlocks = new List<Unlock>();

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)unlocks.Count);
            foreach (Unlock u in unlocks)
                u.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseUnlockService_GetCurrentUserUnlock]";
        }
    }
}
