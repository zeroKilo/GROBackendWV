using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_RewardUserResult
    {
        public uint mRewardID;
        public byte mResult;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mRewardID);
            Helper.WriteU8(s, mResult);
        }
    }
}
