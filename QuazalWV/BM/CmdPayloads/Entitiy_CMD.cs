using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public abstract class Entitiy_CMD
    {
        public uint handle;
        public byte cmd;
        public bool isMaster;
        public bool isServer;

        public void AppendHeader(BitBuffer buf)
        {
            buf.WriteBits(handle, 32);
            buf.WriteBits(cmd, 6);
            buf.WriteBits((uint)(isMaster ? 1 : 0), 1);
            buf.WriteBits((uint)(isServer ? 1 : 0), 1);
        }

        public abstract byte[] MakePayload();

        public static byte[] HandleMsg(ClientInfo client, Stream s)
        {
            List<byte[]> msgs = new List<byte[]>();
            Helper.ReadU32(s);
            uint handle = Helper.ReadU32(s);
            byte cmd = (byte)(Helper.ReadU8(s) & 0x3F);
            Log.WriteLine(1, "Received Cmd 0x" + cmd.ToString("X8") + " for Handle 0x" + handle.ToString("X8"), Color.Red);
            switch (cmd)
            {
                case 0x34:
                    Log.WriteLine(1, "Received Spawn Request", Color.Red);
                    client.playerAbstractState = 3;
                    msgs.Add(DO_RMCRequestMessage.Create(client.callCounterDO_RMC++,
                        0x1006,
                        new DupObj(DupObjClass.Station, 1),
                        new DupObj(DupObjClass.NET_MessageBroker, 5),
                        (ushort)DO_RMCRequestMessage.DOC_METHOD.ProcessMessage,
                        BM_Message.Make(new MSG_ID_Entity_Cmd(client, 0x33))
                        ));
                    if (!client.playerCreateStuffSent2)
                    {
                        client.settings.bitField14.entries[3].word = 1;//server ready
                        client.settings.bitField14.entries[4].word = 1;//client ready
                        client.settings.bitField14.entries[0].word = 1;//spawnCount
                        client.settings.bitField14.entries[1].word = 1;//requestSpawn
                        msgs.Add(DO_RMCRequestMessage.Create(client.callCounterDO_RMC++,
                            0x1006,
                            new DupObj(DupObjClass.Station, 1),
                            new DupObj(DupObjClass.SES_cl_Player_NetZ, 257),
                            (ushort)DO_RMCRequestMessage.DOC_METHOD.SetPlayerParameters,
                            client.settings.toBuffer()
                            ));
                        msgs.Add(DO_RMCRequestMessage.Create(client.callCounterDO_RMC++,
                            0x1006,
                            new DupObj(DupObjClass.Station, 1),
                            new DupObj(DupObjClass.NET_MessageBroker, 5),
                            (ushort)DO_RMCRequestMessage.DOC_METHOD.ProcessMessage,
                            BM_Message.Make(new MSG_ID_Net_Obj_Create(0x2A, 0x05, new OCP_PlayerEntity(2).MakePayload()))
                            ));
                        client.playerCreateStuffSent2 = true;
                    }
                    break;
            }
            if (msgs.Count > 0)
                return DO_BundleMessage.Create(client, msgs);
            else
                return null;
        }
    }
}
