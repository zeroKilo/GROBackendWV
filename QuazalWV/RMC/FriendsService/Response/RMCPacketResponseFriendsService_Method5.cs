using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseFriendsService_Method5 : RMCPResponse
    {
        public List<GR5_FriendData> list = new List<GR5_FriendData>();

        public RMCPacketResponseFriendsService_Method5()
        {
            list.Add(new GR5_FriendData());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count());
            foreach (GR5_FriendData fd in list)
                fd.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseFriendsService_Method5]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
