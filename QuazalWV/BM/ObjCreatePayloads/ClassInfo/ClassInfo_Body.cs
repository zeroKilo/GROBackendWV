using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuazalWV
{
    public class ClassInfo_Body
    {
        enum CHPMT
        {
            HitPoints_F = 0,
            Energy_F = 1,
            HealthRegenBase_F = 2,
            HeadMultiplier_F = 3,
            TorsoMultiplier_F = 4,
            ArmsMultiplier_F = 5,
            LegsMultiplier_F = 6,
            ForeArmsMultiplier_F = 7,
            CalfMultiplier_F = 8,
            PelvisMultiplier_F = 9,
            UpperTorsoMultiplier_F = 10,
            MitigationFactor_F = 11
        }

        const byte memBufferSize = 56;
        uint faceId;
        byte skinId;
        //CHPMT modifiers list (0)
        const byte nbModifiers = 12;
        const ushort bitmask = 0xFFFF;
        List<float> bodyModifiers;

        public ClassInfo_Body()
        {
            faceId = 1;
            skinId = 1;
            bodyModifiers = new List<float>();
            for(byte b = 0; b < nbModifiers; b++)
            {
                switch((CHPMT)b)
                {
                    case CHPMT.HitPoints_F:
                        bodyModifiers.Add(100f);
                        break;
                    case CHPMT.Energy_F:
                        bodyModifiers.Add(100f);
                        break;
                    case CHPMT.HealthRegenBase_F:
                        bodyModifiers.Add(2f);
                        break;
                    case CHPMT.HeadMultiplier_F:
                        bodyModifiers.Add(1.5f);
                        break;
                    case CHPMT.TorsoMultiplier_F:
                        bodyModifiers.Add(1f);
                        break;
                    case CHPMT.ArmsMultiplier_F:
                        bodyModifiers.Add(1f);
                        break;
                    case CHPMT.LegsMultiplier_F:
                        bodyModifiers.Add(1f);
                        break;
                    case CHPMT.ForeArmsMultiplier_F:
                        bodyModifiers.Add(1f);
                        break;
                    case CHPMT.CalfMultiplier_F:
                        bodyModifiers.Add(1f);
                        break;
                    case CHPMT.PelvisMultiplier_F:
                        bodyModifiers.Add(1f);
                        break;
                    case CHPMT.UpperTorsoMultiplier_F:
                        bodyModifiers.Add(1f);
                        break;
                    case CHPMT.MitigationFactor_F:
                        bodyModifiers.Add(1f);
                        break;
                }
            }
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, memBufferSize);
            Helper.WriteU32LE(m, faceId);
            Helper.WriteU8(m, skinId);
            Helper.WriteU8(m, nbModifiers);
            Helper.WriteU16(m, bitmask);
            foreach (float modifier in bodyModifiers)
                Helper.WriteFloat(m, modifier);
            return m.ToArray();//56B
        }
    }
}
