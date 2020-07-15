using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public abstract class BM_Message
    {
        public byte msgType = 0xA;
        public ushort msgID;
        public List<BM_Param> paramList = new List<BM_Param>();
        public static byte[] Make(BM_Message msg)
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, msg.msgType);
            Helper.WriteU16LE(m, msg.msgID);
            foreach (BM_Param p in msg.paramList)
                switch (p.type)
                {
                    case BM_Param.PARAM_TYPE.Integer:
                        Helper.WriteU8(m, 0);
                        Helper.WriteU32LE(m, (uint)(int)p.data);
                        break;
                    case BM_Param.PARAM_TYPE.Float:
                        Helper.WriteU8(m, 0);
                        Helper.WriteFloatLE(m, (float)p.data);
                        break;
                }
            byte[] buff = m.ToArray();
            m = new MemoryStream();
            Helper.WriteU16(m, (ushort)(buff.Length + 2));
            Helper.WriteU16(m, (ushort)buff.Length);
            m.Write(buff, 0, buff.Length);
            Helper.WriteU32(m, 0);
            return m.ToArray();
        }
    }
}
