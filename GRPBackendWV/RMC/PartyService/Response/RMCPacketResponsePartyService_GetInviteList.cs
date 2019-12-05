using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponsePartyService_GetInviteList : RMCPacketReply
    {
        public List<GR5_Invitation> _InvitesList = new List<GR5_Invitation>();

        public RMCPacketResponsePartyService_GetInviteList()
        {
            _InvitesList.Add(new GR5_Invitation());
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)_InvitesList.Count);
            foreach (GR5_Invitation inv in _InvitesList)
                inv.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePartyService_GetInviteList]";
        }
    }
}
