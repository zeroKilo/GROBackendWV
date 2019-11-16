using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseSurveyService_Method1 : RMCPacketReply
    {
        public class Survey
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

        public List<Survey> _outSurveys = new List<Survey>();
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)_outSurveys.Count);
            foreach (Survey s in _outSurveys)
                s.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSurveyService_Method1]";
        }
    }
}
