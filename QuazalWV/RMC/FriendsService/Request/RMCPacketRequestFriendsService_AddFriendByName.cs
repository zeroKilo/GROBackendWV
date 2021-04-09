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
        public uint unk1;
        public string name;

        public RMCPacketRequestFriendsService_AddFriendByName(Stream s)
        {
            unk1 = Helper.ReadU32(s);
            name = Helper.ReadString(s);
        }

        public override string PayloadToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[Friend's name: " + name + "]");
            return "";
        }

        public override byte[] ToBuffer()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[AddFriendByName Request: name=" + name + "]";
        }
    }
}
