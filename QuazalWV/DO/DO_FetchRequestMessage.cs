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
                    Payload_Station ps = new Payload_Station();
                    ps.connectionInfo.m_strStationURL1 = "";
                    ps.connectionInfo.m_strStationURL2 = "";
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_Station, 2), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, ps.Create()));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_SessionClock, 1), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new byte[] { }));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_SES_cl_SessionInfos, 2), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new Payload_SessionInfos().Create()));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_PromotionReferee, 3), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new byte[] { }));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_Session, 4), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new Payload_Session().Create()));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_NET_MessageBroker, 5), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new byte[] { }));
                    uint[] IDGeneratorIDs = { 7, 0xD, 0x13, 0x14, 0x15, 0x16, 0x18, 0x1A, 0x23};
                    foreach (uint id in IDGeneratorIDs)
                        msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_IDGenerator, id), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new byte[] { 0x01, 0x01, 0x00, 0x00, 0x00, 0xFE, 0xFF, 0x3F, 0x00 }));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_IDGenerator, 0x17), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new byte[] { 0x01, 0x02, 0x01, 0x00, 0x00, 0xFE, 0xFF, 0x3F, 0x00 }));
                    msgs.Add(DO_CreateDuplicaMessage.Create(client, Helper.MakeDupObj(DO.CLASS.DOC_IDGenerator, 0x19), Helper.MakeDupObj(DO.CLASS.DOC_Station, 1), 2, new byte[] { 0x01, 0x24, 0x00, 0x00, 0x00, 0xFE, 0xFF, 0x3F, 0x00 }));
                    return DO_BundleMessage.Create(client, msgs);
                default:
                    Log.WriteLine(1, "[DO] Handling DO_FetchRequest unknown dupObj 0x" + dupObj.ToString("X8") + "!");
                    return new byte[0];
            }
        }
    }
}
