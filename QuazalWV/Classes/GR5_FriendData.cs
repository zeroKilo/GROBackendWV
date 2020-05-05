using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_FriendData
    {
        public GR5_BasicPersona m_Person = new GR5_BasicPersona();
        public byte m_Group;

        public void toBuffer(Stream s)
        {
            m_Person.toBuffer(s);
            Helper.WriteU8(s, m_Group);
        }
    }
}
