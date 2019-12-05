using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_MissionArc
    {
        public uint mID;
        public uint mOasisNameID;
        public uint mOasisDescriptionID;
        public uint mClassRequired;
        public byte mFlags;
        public byte mIsLoop;
        public byte mCategory;
        public byte mIsAutoAccept;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mID);
            Helper.WriteU32(s, mOasisNameID);
            Helper.WriteU32(s, mOasisDescriptionID);
            Helper.WriteU32(s, mClassRequired);
            Helper.WriteU8(s, mFlags);
            Helper.WriteU8(s, mIsLoop);
            Helper.WriteU8(s, mCategory);
            Helper.WriteU8(s, mIsAutoAccept);
        }
    }
}
