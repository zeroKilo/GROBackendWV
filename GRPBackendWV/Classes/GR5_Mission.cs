using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Mission
    {
        public uint mID;
        public string mCriteria;
        public uint mOasisName = 70870;
        public uint mOasisDescription = 70870;
        public uint mOasisRequirement = 70870;
        public uint mOasisDebrief = 70870;
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
}
