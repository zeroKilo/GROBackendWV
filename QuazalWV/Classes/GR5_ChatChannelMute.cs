using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_ChatChannelMute
    {
        public uint channel;
        public string reason;
        public uint expiry;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, channel);
            Helper.WriteString(s, reason);
            Helper.WriteU32(s, expiry);
        }
    }
}
