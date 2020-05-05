using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponsePlayerProfileService_GetAllFaceSkinTones : RMCPResponse
    {
        public List<GR5_FaceSkinTone> list = new List<GR5_FaceSkinTone>();

        public RMCPacketResponsePlayerProfileService_GetAllFaceSkinTones()
        {
            list = DBHelper.GetFaceSkinTones();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)list.Count);
            foreach (GR5_FaceSkinTone fst in list)
                fst.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_GetAllFaceSkinTones]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }

}
