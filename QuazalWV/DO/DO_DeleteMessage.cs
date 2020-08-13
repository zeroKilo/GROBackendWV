using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QuazalWV
{
    public class DO_DeleteMessage
    {
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(2, "[DO] Handling DO_DeleteMessage");
            uint handle = BitConverter.ToUInt32(data, 1);
            DupObj obj = DO_Session.FindObj(handle);
            if (obj == null)
            {
                Log.WriteLine(1, "[DO] DO_DeleteMessage sent for an unknown dupobj");
                return new byte[0];
            }
            DO_Session.DupObjs.Remove(obj);
            return null;
        }

        public static byte[] Create(ushort callID, uint fromStationID, uint dupObj, uint toStationID, byte version, List<uint> handles)
        {
            Log.WriteLine(2, "[DO] Creating DO_DeleteMessage TODO");
            return null;
        }
    }
}