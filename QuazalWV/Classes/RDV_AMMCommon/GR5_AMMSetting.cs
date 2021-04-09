using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuazalWV
{
    public class GR5_AMMSetting
    {
        public string msz_KeyName;
        public string msz_Value;
        public string msz_Comment;

        public void toBuffer(Stream s)
        {
            Helper.WriteString(s, msz_KeyName);
            Helper.WriteString(s, msz_Value);
            Helper.WriteString(s, msz_Comment);
        }
    }
}
