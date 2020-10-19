using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class ClassInfo_Gun
    {
        public byte memBufferSize;
        byte nbComponents;//+1
        uint componentListID;//+4
        uint weaponID;//4
        uint oasisNameID;//4
        List<uint> componentIds;

        public ClassInfo_Gun(uint weaponID )
        {
            memBufferSize = 0;
            nbComponents = 9;       //database
            componentListID = 1000; //database
            this.weaponID = weaponID;//database
            oasisNameID = 70910;    //As val
            componentIds = new List<uint>();

            for (uint i = 1; i <= nbComponents; i++) componentIds.Add(i);
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, memBufferSize);
            Helper.WriteU8(m, nbComponents);
            Helper.WriteU32LE(m, componentListID);
            Helper.WriteU32LE(m, weaponID);
            Helper.WriteU32LE(m, oasisNameID);
            foreach(uint compId in componentIds) Helper.WriteU32LE(m, compId);
            return m.ToArray();//49B
        }
    }
}