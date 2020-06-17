using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class ProcessAuthentication
    {
        public byte verStruct;
        public byte verLibProto;
        public byte unk1;
        public uint verMajor;
        public uint verMinor;
        public uint checksum;
        public uint flags;

        public ProcessAuthentication() { }

        public ProcessAuthentication(Stream s) 
        {
            verStruct = Helper.ReadU8(s);
            verLibProto = Helper.ReadU8(s);
            unk1 = Helper.ReadU8(s);
            verMajor = Helper.ReadU32(s);
            verMinor = Helper.ReadU32(s);
            checksum = Helper.ReadU32(s);
            flags = Helper.ReadU32(s);
        }

        public void toBuffer(Stream s)
        {
            Helper.WriteU8(s, verStruct);
            Helper.WriteU8(s, verLibProto);
            Helper.WriteU8(s, unk1);
            Helper.WriteU32(s, verMajor);
            Helper.WriteU32(s, verMinor);
            Helper.WriteU32(s, checksum);
            Helper.WriteU32(s, flags);
        }

        public string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "[Process Authentication]");
            sb.AppendLine(tabs + " Version Structure    = 0x" + verStruct.ToString("X2"));
            sb.AppendLine(tabs + " Version Lib Protocol = 0x" + verLibProto.ToString("X2"));
            sb.AppendLine(tabs + " Unknown 1            = 0x" + unk1.ToString("X2"));
            sb.AppendLine(tabs + " Version Major        = 0x" + verMajor.ToString("X8"));
            sb.AppendLine(tabs + " Version Minor        = 0x" + verMinor.ToString("X8"));
            sb.AppendLine(tabs + " Titel Checksum       = 0x" + checksum.ToString("X8"));
            sb.AppendLine(tabs + " Protocol Flags       = 0x" + flags.ToString("X8"));
            return sb.ToString();
        }
    }
}
