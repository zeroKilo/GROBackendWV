using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class SessionParameters
    {
        public uint dword0;                 //0x00
        public uint mapKey;                 //0x04
        public uint matchID;                //0x08
        public uint someOtherKey;           //0x0C
        public uint uint10;                 //0x10
        public uint uint14;                 //0x14
        public uint LastDSLiveLogEventTime; //0x18
        public uint LastStatUpdateTime;     //0x1C
        public uint playlistIndex;          //0x20
        public byte gameMode;               //0x24
        public byte byte25;                 //0x25
        public byte byte26;                 //0x26
        public byte byte27;                 //0x27
        public byte byte28;                 //0x28
        public byte byte29;                 //0x29
        public byte byte2A;                 //0x2A
        public byte byte2B;                 //0x2B
        public byte byte2C;                 //0x2C

        private void CopyToBuffer(byte[] buff, int pos, uint value, bool swapEndianess = false)
        {
            if (!swapEndianess)
            {
                buff[pos] = (byte)value;
                buff[pos + 1] = (byte)(value >> 8);
                buff[pos + 2] = (byte)(value >> 16);
                buff[pos + 3] = (byte)(value >> 24);
            }
            else
            {
                buff[pos] = (byte)(value >> 24);
                buff[pos + 1] = (byte)(value >> 16);
                buff[pos + 2] = (byte)(value >> 8);
                buff[pos + 3] = (byte)value;
            }
        }

        public void toBuffer(Stream s)
        {
            byte[] buff = new byte[256];
            CopyToBuffer(buff, 0x00, dword0);
            CopyToBuffer(buff, 0x04, mapKey, true);
            CopyToBuffer(buff, 0x08, matchID);
            CopyToBuffer(buff, 0x0C, someOtherKey);
            CopyToBuffer(buff, 0x10, uint10);
            CopyToBuffer(buff, 0x14, uint14);
            CopyToBuffer(buff, 0x18, LastDSLiveLogEventTime);
            CopyToBuffer(buff, 0x1C, LastStatUpdateTime);
            CopyToBuffer(buff, 0x20, playlistIndex);
            buff[0x24] = gameMode;
            buff[0x25] = byte25;
            buff[0x26] = byte26;
            buff[0x27] = byte27;
            buff[0x28] = byte28;
            buff[0x29] = byte29;
            buff[0x2A] = byte2A;
            buff[0x2B] = byte2B;
            buff[0x2C] = byte2C;
            s.Write(buff, 0, 256);
        }
    }

    public class SessionInfosParameter
    {
        public static uint defaultMapKey = 0xDE139C36;
        public SessionParameters sParams = new SessionParameters();
        public bool m_bSessionParametersAreSet;
        public SessionInfosParameter()
        {
            m_bSessionParametersAreSet = true;
            //AF020A0E Data/99 - Standalone/Menu/TheProvingGrounds/Modelisation/World//
            //DE139C36 Data/- 14 - Maps GRO/03_MoscowUB_City/03_MoscowUB_City_LD/Modelisation/World//
            //9203DA88 Data/- 14 - Maps GRO/03_MoscowUB_City/03_MoscowUB_City_Global/Modelisation/World//
            //4E100B51 Data/99 - Standalone/Menu/GlobalGUI/Modelisation/World//
            //B2001CDC Data/99 - Standalone/Dedicated Server/Menu/Modelisation/World//
            sParams.mapKey = defaultMapKey;
            sParams.matchID = 1;
            sParams.playlistIndex = 0;
            sParams.gameMode = 9;
        }

        public void toBuffer(Stream s)
        {
            s.WriteByte((byte)(m_bSessionParametersAreSet ? 1 : 0));
            sParams.toBuffer(s);
        }

        public string getDesc(string tabs = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tabs + "[SessionInfosParameter]");
            sb.AppendLine(tabs + " SessionParameters       = (TODO)" );
            sb.AppendLine(tabs + " SessionParametersAreSet = " + m_bSessionParametersAreSet);
            return sb.ToString();
        }
    }
}
