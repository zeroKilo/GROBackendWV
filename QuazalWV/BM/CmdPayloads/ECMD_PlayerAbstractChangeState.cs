using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class ECMD_PlayerAbstractChangeState : Entitiy_CMD
    {
        public byte state;

        public ECMD_PlayerAbstractChangeState(uint h, byte s, bool isM = false, bool isS = false)
        {
            handle = h;
            cmd = 0x33;
            isMaster = isM;
            isServer = isS;

            state = s;
        }
        public override byte[] MakePayload()
        {
            BitBuffer buf = new BitBuffer();
            AppendHeader(buf);
            buf.WriteBits(state, 4);
            return buf.toArray();
        }
    }
}
