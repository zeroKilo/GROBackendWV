using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    class ECMD_PlayerFallingDamage : Entitiy_CMD
    {
        public float damage;
        public ECMD_PlayerFallingDamage(uint h, float dmg, bool isM = false, bool isS = false)
        {
            handle = h;
            cmd = 0x1C;
            isMaster = isM;
            isServer = isS;

            damage = dmg;
        }

        public override byte[] MakePayload()
        {
            BitBuffer buf = new BitBuffer();
            AppendHeader(buf);
            byte[] buff = BitConverter.GetBytes(damage);
            int raw = BitConverter.ToInt32(buff, 0);
            buf.WriteBits((uint)raw, 32);
            return buf.toArray();
        }
    }
}