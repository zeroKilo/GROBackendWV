using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_FetchRequestMessage
    {
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            List<byte[]> msgs;
            Log.WriteLine(1, "[DO] Handling DO_FetchRequestMessage...");
            MemoryStream m = new MemoryStream(data);
            m.Seek(3, 0);
            uint dupObj = Helper.ReadU32(m);
            switch (dupObj)
            {
                case 0x5C00001:
                    msgs = new List<byte[]>();
                    foreach (DupObj obj in DO_Session.DupObjs)
                        msgs.Add(DO_CreateDuplicaMessage.Create(obj, 2));
                    msgs.Add(DO_MigrationMessage.Create(client.callCounterDO_RMC++, new DupObj(DupObjClass.Station, 1), new DupObj(DupObjClass.Station, client.stationID), new DupObj(DupObjClass.Station, client.stationID), 3, new List<uint>() { new DupObj(DupObjClass.Station, client.stationID) }));
                    return DO_BundleMessage.Create(client, msgs);
                default:
                    Log.WriteLine(1, "[DO] Handling DO_FetchRequest unknown dupObj 0x" + dupObj.ToString("X8") + "!");
                    return new byte[0];
            }
        }

        public static byte[] Create(ushort callID, DupObj obj)
        {
            Log.WriteLine(1, "[DO] Creating DO_FetchRequestMessage");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0xD);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, obj);
            Helper.WriteU32(m, obj.Master);
            return m.ToArray();
        }
    }
}
