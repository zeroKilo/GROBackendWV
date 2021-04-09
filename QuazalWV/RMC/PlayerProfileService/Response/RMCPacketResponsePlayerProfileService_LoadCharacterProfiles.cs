using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class RMCPacketResponsePlayerProfileService_GetProfileData : RMCPResponse
    {
        public GR5_Persona persona;
        public List<GR5_Character> characters;

        public RMCPacketResponsePlayerProfileService_GetProfileData(ClientInfo client)
        {
            persona = DBHelper.GetPersona(client);
            characters = DBHelper.GetCharacters(client.PID);
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            persona.toBuffer(m);
            Helper.WriteU32(m, (uint)characters.Count);
            foreach (GR5_Character c in characters)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponsePlayerProfileService_GetProfileData]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
