using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketRequestFriendsService_AddFriendByName : RMCPRequest
    {
        public List<string> Names { get; set; }

        public RMCPacketRequestFriendsService_AddFriendByName(Stream s)
        {
            Names = new List<string>();
            uint count = Helper.ReadU32(s);
            for (uint i = 0; i < count; i++)
                Names.Add(Helper.ReadString(s));
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t[Friend: {Names[0]}]");
            return sb.ToString();
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[AddFriendByName Request]";
        }
    }
}
