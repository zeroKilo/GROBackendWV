using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class GR5_Survey
    {
        public uint mId;
        public byte mWeight;
        public string mSurveyTrigger;
        public string mSurveyURL;
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mId);
            Helper.WriteU8(s, mWeight);
            Helper.WriteString(s, mSurveyTrigger);
            Helper.WriteString(s, mSurveyURL);
        }
    }
}
