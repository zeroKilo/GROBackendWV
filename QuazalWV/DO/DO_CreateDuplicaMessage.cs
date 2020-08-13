using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_CreateDuplicaMessage
    {        
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(2, "[DO] Handling DO_CreateDuplicaMessage...");
            MemoryStream m = new MemoryStream(data);
            m.Seek(1, 0);
            DupObj obj = new DupObj(Helper.ReadU32(m));
            obj.Master = new DupObj(Helper.ReadU32(m));
            DO_Session.DupObjs.Add(obj);
            Log.WriteLine(1, "[DO] Adding DupObj " + obj.getDesc());
            return null;
        }

        public static byte[] Create(DupObj obj, byte version)
        {
            Log.WriteLine(2, "[DO] Creating DO_CreateDuplicaMessage");
            MemoryStream m = new MemoryStream();
            m.WriteByte(0x12);
            Helper.WriteU32(m, obj);
            Helper.WriteU32(m, obj.Master);
            Helper.WriteU8(m, version);
            byte[] payload = obj.getPayload();
            m.Write(payload, 0, payload.Length);
            return m.ToArray();
        }
    }
}
