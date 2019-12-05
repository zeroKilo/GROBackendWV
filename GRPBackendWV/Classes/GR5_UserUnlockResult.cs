using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_UserUnlockResult
    {
        public uint mUnlockID;
        public byte mResult;
        public void ToBuffer(Stream stream)
        {
            Helper.WriteU32(stream, mUnlockID);
            Helper.WriteU8(stream, mResult);
        }
    }
}
