using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAvatarService_GetAvatarPortraits : RMCPResponse
    {
        public List<GR5_AvatarPortrait> portraits = new List<GR5_AvatarPortrait>();
        public List<uint> defaultPortraits = new List<uint>();

        public RMCPacketResponseAvatarService_GetAvatarPortraits()
        {
            for(uint i = 1; i < 36; i++)
            {
                portraits.Add(new GR5_AvatarPortrait(i, i));
                defaultPortraits.Add(i);
            }
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)portraits.Count);
            foreach (GR5_AvatarPortrait p in portraits)
                p.toBuffer(m);
            Helper.WriteU32(m, (uint)defaultPortraits.Count);
            foreach (uint u in defaultPortraits)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAvatarService_GetAvatarPortraits]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
