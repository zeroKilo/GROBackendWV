using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseMissionService_GetAllMissionTemplate : RMCPacketReply
    {
        public class Mission
        {
            public uint mID;
            public string mCriteria;
            public uint mOasisName;
            public uint mOasisDescription;
            public uint mOasisRequirement;
            public uint mOasisDebrief;
            public byte mMinLevel;
            public byte mMaxLevel;
            public byte mMinParty;
            public byte mCommandoRequired;
            public byte mReconRequired;
            public byte mSpecialistRequired;
            public byte mFlags;
            public uint mAssetId;

            public void toBuffer(Stream s)
            {
                Helper.WriteU32(s, mID);
                Helper.WriteString(s, mCriteria);
                Helper.WriteU32(s, mOasisName);
                Helper.WriteU32(s, mOasisDescription);
                Helper.WriteU32(s, mOasisRequirement);
                Helper.WriteU32(s, mOasisDebrief);
                Helper.WriteU8(s, mMinLevel);
                Helper.WriteU8(s, mMaxLevel);
                Helper.WriteU8(s, mMinParty);
                Helper.WriteU8(s, mCommandoRequired);
                Helper.WriteU8(s, mReconRequired);
                Helper.WriteU8(s, mSpecialistRequired);
                Helper.WriteU8(s, mFlags);
                Helper.WriteU32(s, mAssetId);
            }
        }

        public class MissionArc
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

        public class MissionSequence
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

        public List<Mission> missions = new List<Mission>();
        public List<MissionArc> missionArcs = new List<MissionArc>();
        public List<MissionSequence> missionSeqs = new List<MissionSequence>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)missions.Count);
            foreach (Mission mm in missions)
                mm.toBuffer(m);
            Helper.WriteU32(m, (uint)missionArcs.Count);
            foreach (MissionArc ma in missionArcs)
                ma.toBuffer(m);
            Helper.WriteU32(m, (uint)missionSeqs.Count);
            foreach (MissionSequence s in missionSeqs)
                s.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseMissionService_GetAllMissionTemplate]";
        }
    }
}
