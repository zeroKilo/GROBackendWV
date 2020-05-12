using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class StationInfo
    {
        public uint m_hObserver;
        public uint m_uiMachineUID;

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, m_hObserver);
            Helper.WriteU32(s, m_uiMachineUID);
        }
    }
}
