using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class BM_Param
    {
        public enum PARAM_TYPE
        {
            Integer = 1,
            Float = 2,
            Object = 3,
            Vector = 4,
            Struct = 5,
            String = 6,
            Buffer = 0x80
        }

        public PARAM_TYPE type;
        public byte bufferIndex;
        public object data;

        public BM_Param(PARAM_TYPE t, object d)
        {
            type = t;
            data = d;
        }
    }
}
