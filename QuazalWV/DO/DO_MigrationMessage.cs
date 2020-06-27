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
            List<byte[]> msgs = new List<byte[]>();
            msgs.Add(DO_Outcome.Create(callID, 0x60001));
            if (fobj != null && fobj.Class == DupObjClass.SES_cl_Player_NetZ && fobj.ID == 257 && !client.matchStartSent)
            {
                /*
                msgs.Add(DO_RMCRequestMessage.Create(client.callCounterDO_RMC++,
                    0x1006,
                    new DupObj(DupObjClass.Station, 1),
                    new DupObj(DupObjClass.SES_cl_Player_NetZ, 257),
                    (ushort)DO_RMCRequestMessage.DOC_METHOD.SetPlayerState,
                    new byte[] { 0x34, 0x12, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0xC0, 0x05, 0x00, 0x10, 0x00, 0x00, 0x00, 0x22, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }
                    ));
                msgs.Add(DO_RMCRequestMessage.Create(client.callCounterDO_RMC++,
                    0x1006,
                    new DupObj(DupObjClass.Station, 1),
                    new DupObj(DupObjClass.SES_cl_Player_NetZ, 257),
                    (ushort)DO_RMCRequestMessage.DOC_METHOD.SetPlayerRDVInfo,
                    new byte[] { 0x01, 0x00, 0x00, 0x00 }
                    ));
                 */
                SessionInfosParameter p = new SessionInfosParameter();
                p.sParams.byte25 = 1;
                m = new MemoryStream();
                m.WriteByte(2);//update
                Helper.WriteU32(m, new DupObj(DupObjClass.SES_cl_SessionInfos, 2));
                m.WriteByte(2);//part
                p.toBuffer(m);
                msgs.Add(m.ToArray());

                p.sParams.byte25 = 2;
                m = new MemoryStream();
                m.WriteByte(2);//update
                Helper.WriteU32(m, new DupObj(DupObjClass.SES_cl_SessionInfos, 2));
                m.WriteByte(2);//part
                p.toBuffer(m);
                msgs.Add(m.ToArray());

                msgs.Add(DO_RMCRequestMessage.Create(client.callCounterDO_RMC++,
                    0x1006,
                    new DupObj(DupObjClass.Station, 1),
                    new DupObj(DupObjClass.SES_cl_Player_NetZ, 257),
                    (ushort)DO_RMCRequestMessage.DOC_METHOD.SetPlayerParameters,
                    new byte[] { 0x34, 0x12, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0xC0, 0x05, 0x00, 0x10, 0x00, 0x00, 0x00, 0x21, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }
                    ));

                msgs.Add(DO_RMCRequestMessage.Create(client.callCounterDO_RMC++,
                    0x806,
                    new DupObj(DupObjClass.Station, 1),
                    new DupObj(DupObjClass.SES_cl_SessionInfos, 2),
                    (ushort)DO_RMCRequestMessage.DOC_METHOD.OnStartMatch,
                    new byte[] { }
                    ));
                client.matchStartSent = true;
            }
            return DO_BundleMessage.Create(client, msgs);
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
