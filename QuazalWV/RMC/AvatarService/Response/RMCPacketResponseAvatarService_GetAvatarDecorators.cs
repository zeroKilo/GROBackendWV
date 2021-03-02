using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseAvatarService_GetAvatarDecorators : RMCPResponse
    {
        public List<GR5_AvatarDecorator> decos = new List<GR5_AvatarDecorator>();
        public List<uint> defaultDecos = new List<uint>();

        public RMCPacketResponseAvatarService_GetAvatarDecorators()
        {
            decos.Add(new GR5_AvatarDecorator());
            defaultDecos.Add(0);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)decos.Count);
            foreach (GR5_AvatarDecorator d in decos)
                d.toBuffer(m);
            Helper.WriteU32(m, (uint)defaultDecos.Count);
            foreach (uint u in defaultDecos)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAvatarService_GetAvatarDecorators]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
