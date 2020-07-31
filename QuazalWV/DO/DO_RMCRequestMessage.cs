using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class DO_RMCRequestMessage
    {
        public enum DOC_METHOD
        {
            AddDuplicaLocation = 1,
            DeleteDuplica = 2,
            RemoveFromCachedDuplicationSet = 3,
            AdjustTime = 4,
            SyncRequest = 5,
            SyncResponse = 6,
            AskForSettingPlayerParameters = 7,
            AskForSettingPlayerState = 8,
            SetPlayerParameters = 9,
            SetPlayerState = 10,
            SetPlayerIdentity = 11,
            SetPlayerRDVInfo = 12,
            AskForSettingSessionParameters = 13,
            Disconnect = 14,
            IncreasePlayerNb = 15,
            OnEndMatch = 16,
            OnStartMatch = 17,
            PlayVoiceChat = 18,
            PlayVoiceChatWithMutedPlayers = 19,
            UpdateSessionHostAfterMigration = 20,
            RequestIDRangeFromMaster = 21,
            ConfirmElection = 22,
            DeclinePromotion = 23,
            ElectNewMaster = 24,
            KickOut = 25,
            ReportFault = 26,
            RetrieveURLs = 27,
            SynchronizeTermination = 28,
            SignalAsFaulty = 29,
            ProcessMessage = 30,
            RouteMessage = 31
        }

        public static byte[] HandleMessage(ClientInfo client, QPacket p, byte[] data)
        {
            Log.WriteLine(1, "[DO] Handling DO_RMCRequestMessage...");
            MemoryStream m = new MemoryStream(data);
            m.Seek(1, 0);
            ushort callID = Helper.ReadU16(m);
            uint flags = Helper.ReadU32(m);
            uint station = Helper.ReadU32(m);
            uint targetObject = Helper.ReadU32(m);
            DOC_METHOD method = (DOC_METHOD)Helper.ReadU16(m);
            Log.WriteLine(2, "[DO] RMC Call ID      : 0x" + callID.ToString("X4"));
            Log.WriteLine(2, "[DO] RMC Call Flags   : 0x" + flags.ToString("X8"));
            Log.WriteLine(2, "[DO] RMC Call Station : 0x" + station.ToString("X8"));
            Log.WriteLine(2, "[DO] RMC Call DupObj  : 0x" + targetObject.ToString("X8") + " " + new DupObj(targetObject).getDesc());
            byte[] buff;
            MemoryStream m2;
            List<byte[]> msgs;
            switch (method)
            {
                case DOC_METHOD.SyncRequest:
                    Log.WriteLine(1, "[DO] Handling SyncRequest...");
                    ulong time = Helper.ReadU64(m);
                    buff = Create(client.callCounterDO_RMC++, 0x83C, new DupObj(DupObjClass.Station, 1), new DupObj(DupObjClass.SessionClock, 1), 6, new Payload_SyncResponse(time).toBuffer());
                    m2 = new MemoryStream();
                    Helper.WriteU32(m2, (uint)buff.Length);
                    m2.Write(buff, 0, buff.Length);
                    m2.WriteByte((byte)QPacket.MakeChecksum(m2.ToArray(), 0));
                    p.payload = m2.ToArray();
                    p.payloadSize = (ushort)p.payload.Length;
                    p.m_uiSignature = client.IDsend;
                    DO.Send(p, client);
                    return null;
                case DOC_METHOD.RequestIDRangeFromMaster:
                    Log.WriteLine(1, "[DO] Handling RequestIDRangeFromMaster...");
                    return DO_RMCResponseMessage.Create(callID, 0x60001, new byte[] { 0x01, 0x01, 0x00, 0x00, 0x01, 0x02, 0x00, 0x00 });
                case DOC_METHOD.IncreasePlayerNb:
                    Log.WriteLine(1, "[DO] Handling IncreasePlayerNb...");
                    msgs = new List<byte[]>();
                    msgs.Add(new byte[] { 0x02, 0x02, 0x00, 0x40, 0x05, 0x01, 0x01, 0x00, 0x00, 0x00 });
                    msgs.Add(DO_RMCResponseMessage.Create(callID, 0x60001, new byte[] { 0x00 }));                    
                    return DO_BundleMessage.Create(client, msgs);
                case DOC_METHOD.AskForSettingPlayerParameters:
                    Log.WriteLine(1, "[DO] Handling AskForSettingPlayerParameters...");
                    int len = (int)(data.Length - m.Position);
                    buff = new byte[len];
                    m.Read(buff, 0, len);
                    client.settings = new Payload_PlayerParameter(buff);
                    client.settings.bitField14.entries[3].word = 1;//server ready
                    client.settings.bitField14.entries[2].word = 1;//ammstatus
                    ////client.settings.bitField10.entries[2].word = 2;//teamIndex
                    //if (client.settings.bitField10.entries[4].word == 1) //change state?
                    //{
                    //    //client.settings.bitField10.entries[4].word = 0;//change state
                    //    //client.settings.bitField10.entries[5].word = 1;//isSync
                    //    //client.settings.bitField14.entries[0].word = 1;//spawnCount
                    //    //client.settings.bitField14.entries[1].word = 1;//requestSpawn
                    //    //client.settings.bitField14.entries[2].word = 1;//ammstatus
                    //    //client.settings.bitField14.entries[4].word = 1;//client ready
                    //}
                    msgs = new List<byte[]>();
                    msgs.Add(DO_RMCRequestMessage.Create(client.callCounterDO_RMC++,
                        0x1006,
                        new DupObj(DupObjClass.Station, 1),
                        new DupObj(DupObjClass.SES_cl_Player_NetZ, 257),
                        (ushort)DO_RMCRequestMessage.DOC_METHOD.SetPlayerParameters,
                        client.settings.toBuffer()
                        ));
                    msgs.Add(DO_RMCResponseMessage.Create(callID, 0x60001, new byte[] { 0x00 }));
                    return DO_BundleMessage.Create(client, msgs);
                case DOC_METHOD.AskForSettingPlayerState:
                    Log.WriteLine(1, "[DO] Handling AskForSettingPlayerState...");
                    msgs = new List<byte[]>();
                    msgs.Add(DO_RMCRequestMessage.Create(client.callCounterDO_RMC++,
                        0x1006,
                        new DupObj(DupObjClass.Station, 1),
                        new DupObj(DupObjClass.SES_cl_Player_NetZ, 257),
                        (ushort)DO_RMCRequestMessage.DOC_METHOD.SetPlayerState,
                        BitConverter.GetBytes(Helper.ReadU32(m))
                        ));
                    msgs.Add(DO_RMCResponseMessage.Create(callID, 0x60001, new byte[] { 0x00 }));
                    return DO_BundleMessage.Create(client, msgs);
                case DOC_METHOD.AskForSettingSessionParameters:
                    Log.WriteLine(1, "[DO] Handling AskForSettingSessionParameters...");
                    len = (int)(data.Length - m.Position);
                    buff = new byte[len];
                    m.Read(buff, 0, len);
                    msgs = new List<byte[]>();

                    m = new MemoryStream();
                    m.WriteByte(2);//update
                    Helper.WriteU32(m, new DupObj(DupObjClass.SES_cl_SessionInfos, 2));
                    m.WriteByte(2);//part
                    m.WriteByte(1);//params set
                    m.Write(buff, 0, len);
                    msgs.Add(m.ToArray());

                    msgs.Add(DO_RMCResponseMessage.Create(callID, 0x60001, new byte[] { 0x00 }));
                    return DO_BundleMessage.Create(client, msgs);
                case DOC_METHOD.ProcessMessage:
                    Log.WriteLine(1, "[DO] Handling ProcessMessage...");
                    return BM_Message.HandleMessage(client, m);
                default:
                    Log.WriteLine(1, "[DO] Error: Unhandled DOC method: " + method + "!");
                    return null;
            }
        }

        public static byte[] Create(ushort callID, uint flags, uint station, uint target, ushort method, byte[] payload)
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, 0xA);
            Helper.WriteU16(m, callID);
            Helper.WriteU32(m, flags);
            Helper.WriteU32(m, station);
            Helper.WriteU32(m, target);
            Helper.WriteU16(m, method);
            m.Write(payload, 0, payload.Length);
            return m.ToArray();
        }
    }
}
