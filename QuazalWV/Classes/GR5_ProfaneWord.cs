using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_ProfaneWord
    {
        public uint mId;
        public byte mType;
        public string mWord = "rendel";
        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, mId);
            Helper.WriteU8(s, mType);
            Helper.WriteString(s, mWord);
        }
    }
}
