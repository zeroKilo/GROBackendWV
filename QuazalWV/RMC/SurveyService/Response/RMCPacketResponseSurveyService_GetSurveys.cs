﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponseSurveyService_GetSurveys : RMCPResponse
    {
        public List<GR5_Survey> _outSurveys = new List<GR5_Survey>();

        public RMCPacketResponseSurveyService_GetSurveys()
        {
            _outSurveys.Add(new GR5_Survey());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)_outSurveys.Count);
            foreach (GR5_Survey s in _outSurveys)
                s.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseSurveyService_GetSurveys]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
