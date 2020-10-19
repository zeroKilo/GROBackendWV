using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    class ClassInfo_Grenade
    {
        public byte memBufferSize;
        byte nbComponents;
        uint componentListID;
        uint weaponID;
        uint oasisNameID;
        List<uint> componentIds;
        byte someCount;

        public ClassInfo_Grenade(uint weaponID)
        {
            memBufferSize = 50;
            nbComponents = 9;       //database
            componentListID = 1000; //database
            this.weaponID = weaponID;
            oasisNameID = 70910;    //As val
            componentIds = new List<uint>();

            for (uint i = 1; i <= nbComponents; i++) componentIds.Add(i);
            someCount = 0;
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, memBufferSize);
            Helper.WriteU8(m, nbComponents);
            Helper.WriteU32LE(m, componentListID);
            Helper.WriteU32LE(m, weaponID);
            Helper.WriteU32LE(m, oasisNameID);
            foreach (uint compId in componentIds) Helper.WriteU32LE(m, compId);
            Helper.WriteU8(m, someCount);
            return m.ToArray();
        }
    }
}