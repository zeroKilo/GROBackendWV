using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestFriendsService_MoveFriendToGroup : RMCPRequest
    {
        public List<uint> Pids { get; set; }
        public byte Group { get; set; }

        public RMCPacketRequestFriendsService_MoveFriendToGroup(Stream s)
        {
            Pids = new List<uint>();
            uint count = Helper.ReadU32(s);
            for (uint idx = 0; idx < count; idx++)
                Pids.Add(Helper.ReadU32(s));
            Group = Helper.ReadU8(s);
        }

        public override string PayloadToString()
        {
            return "";
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[MoveFriendToGroup Request]";
        }
    }
}
