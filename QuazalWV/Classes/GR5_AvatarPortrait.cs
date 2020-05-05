using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_AvatarPortrait
    {
        public uint mItemID;
        public uint mPortraitID;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mItemID);
            Helper.WriteU32(s, mPortraitID);
        }
    }
}
