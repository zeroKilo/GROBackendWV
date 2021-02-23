using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    class RMCPacketResponseAMM_ds_AddParticipantToSession : RMCPResponse
    {

        public override byte[] ToBuffer()
        {
            return null;
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAMMDS_ds_AddParticipantToSession]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
