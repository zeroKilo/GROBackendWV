using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class MSG_ID_Entity_Cmd : BM_Message
    {
        public MSG_ID_Entity_Cmd(ClientInfo client, byte cmdID)
        {
            msgID = 0x96;
            paramList.Add(new BM_Param(BM_Param.PARAM_TYPE.Buffer, MakePayload(client, cmdID)));
        }

        public byte[] MakePayload(ClientInfo client, byte cmdID)
        {
            Entitiy_CMD cmd = null;
            switch (cmdID)
            {
                case 0x33:
                    cmd = new ECMD_PlayerAbstractChangeState(1, client.playerAbstractState);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return cmd.MakePayload();
        }
    }
}
