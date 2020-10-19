using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class ClassInfo_Armor
    {
        public byte memBufferSize;
        uint armorItemId;
        byte camoId;
        byte pairCount;//max 6
        List<Tuple<uint, uint>> pairs;//thats another mistake but we dont send this yet, gonna correct anyway
        float bonusHealth;
        float bonusHealthRegen;
        float toughness;
        float criticalMitigation;
        //combat property modifiers
        byte nbModifiers;
        ushort bitmask;
        List<float> propertyList;

        public ClassInfo_Armor()
        {
            memBufferSize = 73;
            armorItemId = 1;//COM-01-Helm00
            camoId = 1;
            pairCount = 0;
            pairs = new List<Tuple<uint, uint>>();
            bonusHealth = 10.0f;
            bonusHealthRegen = 0.5f;
            toughness = 5.0f;
            criticalMitigation = 3.0f;
            nbModifiers = 12;
            bitmask = 0xFFFF;
            propertyList = new List<float>();
            float tmp = 0;
            for (byte b = 0; b < nbModifiers; b++)
            {
                propertyList.Add(tmp);
                tmp++;
            }
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, memBufferSize);
            Helper.WriteU32LE(m, armorItemId);
            Helper.WriteU8(m, camoId);
            Helper.WriteU8(m, pairCount);
            if(pairCount>0)
            {
                foreach(Tuple<uint, uint> pair in pairs)
                {
                    Helper.WriteU32LE(m, pair.Item1);
                    Helper.WriteU32LE(m, pair.Item2);
                }
            }
            Helper.WriteFloatLE(m, bonusHealth);
            Helper.WriteFloatLE(m, bonusHealthRegen);
            Helper.WriteFloatLE(m, toughness);
            Helper.WriteFloatLE(m, criticalMitigation);
            Helper.WriteU8(m, nbModifiers);
            Helper.WriteU16(m, bitmask);
            foreach (float mod in propertyList) Helper.WriteFloat(m, mod);
            return m.ToArray();//73B
        }
    }
}
