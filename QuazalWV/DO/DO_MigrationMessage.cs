using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_MigrationMessage
    {    
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_MigrationMessage");
            MemoryStream m = new MemoryStream(data);
            m.Seek(1, 0);
            ushort callID = Helper.ReadU16(m);
            DupObj from = new DupObj(Helper.ReadU32(m));
            DupObj obj = new DupObj(Helper.ReadU32(m));
            obj.Master = from;
            DupObj to = new DupObj(Helper.ReadU32(m));
            DupObj fobj = DO_Session.FindObj(obj);
            if (fobj == null)
                Log.WriteLine(1, "[DO] DupObj " + obj.getDesc() + " not found!", Color.Red);
            else if (fobj.Master == (uint)to)
                Log.WriteLine(1, "[DO] Master of DupObj " + fobj.getDesc() + " alread set, ignored!", Color.Orange);
            else
                fobj.Master = to;
            return DO_Outcome.Create(callID, 0x60001);
        }

        public static byte[] Create(ushort callID, uint fromStationID, uint dupObj, uint toStationID, byte version, List<uint> handles)
        {
            Log.WriteLine(1, "[DO] Creating DO_MigrationMessage");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x11);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, fromStationID);
            Helper.WriteU32(m, dupObj);
            Helper.WriteU32(m, toStationID);
            m.WriteByte(version);
            Helper.WriteU32(m, (uint)handles.Count);
            foreach (uint u in handles)
                Helper.WriteU32(m, u);
            return m.ToArray();
        }
    }
}
