using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_JoinRequestMessage
    {
        public static byte[] HandleMessage(ClientInfo client, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_JoinRequestMessage...");
            List<byte[]> msgs = new List<byte[]>();
            msgs.Add(DO_JoinResponseMessage.Create(1, Helper.MakeDupObj(DO.CLASS.DOC_Station, 2), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1)));
            msgs.Add(DO_MigrationMessage.Create(3, Helper.MakeDupObj(DO.CLASS.DOC_Station, 2), Helper.MakeDupObj(DO.CLASS.DOC_Station, 2), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 3));
            return DO_BundleMessage.Create(client, msgs);
        }
    }
}
