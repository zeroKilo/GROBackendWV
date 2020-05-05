using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_PersonaMission
    {
        public uint mMissionSequenceId;
        public byte mMissionStatus;
        public uint mStartDateTime;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mMissionSequenceId);
            Helper.WriteU8(s, mMissionStatus);
            Helper.WriteU32(s, mStartDateTime);
        }
    }
}
