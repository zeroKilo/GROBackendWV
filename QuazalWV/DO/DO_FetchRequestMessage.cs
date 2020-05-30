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
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new Payload_Station().Create()));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_SessionClock, 1), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new byte[] { }));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_Session, 4), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new Payload_Session().Create()));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_PromotionReferee, 1), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new byte[] { }));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_NET_MessageBroker, 1), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new byte[] { }));
                    return DO_BundleMessage.Create(client, msgs);
                default:
                    Log.WriteLine(1, "[DO] Handling DO_FetchRequest unknown dupObj 0x" + dupObj.ToString("X8") + "!");
                    return new byte[0];
            }
        }
    }
}
