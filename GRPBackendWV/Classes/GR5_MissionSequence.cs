using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_MissionSequence
    {
        public uint mID;
        public uint mMissionArcId;
        public uint mMissionId;
        public uint mOrder;
        public byte mCompleteRequired;
        public uint mStartTime;
        public uint mEndTime;
        public uint mTimeLimit;
        public uint mSKUId;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mID);
            Helper.WriteU32(s, mMissionArcId);
            Helper.WriteU32(s, mMissionId);
            Helper.WriteU32(s, mOrder);
            Helper.WriteU8(s, mCompleteRequired);
            Helper.WriteU32(s, mStartTime);
            Helper.WriteU32(s, mEndTime);
            Helper.WriteU32(s, mTimeLimit);
            Helper.WriteU32(s, mSKUId);
        }
    }
}
