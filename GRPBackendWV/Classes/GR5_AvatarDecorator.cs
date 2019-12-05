using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_AvatarDecorator
    {
        public uint mItemID;
        public uint mDecoratorID;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mItemID);
            Helper.WriteU32(s, mDecoratorID);
        }
    }
}
