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
        public byte byte3;                  //0x00
        public uint mapKey;                 //0x01
        public uint matchID;                //0x05
        public uint someOtherKey;           //0x09
        public uint uint10;                 //0x0D
        public uint uint14;                 //0x11
        public uint LastDSLiveLogEventTime; //0x15
        public uint LastStatUpdateTime;     //0x19
        public uint playlistIndex;          //0x1D
        public byte gameMode;               //0x21
        public byte byte25;                 //0x22
        public byte byte26;                 //0x23
        public byte byte27;                 //0x24
        public byte byte28;                 //0x25
        public byte byte29;                 //0x26
        public byte byte2A;                 //0x27
        public byte byte2B;                 //0x28
        public byte byte2C;                 //0x29

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
            buff[0x00] = byte3;
            CopyToBuffer(buff, 0x01, mapKey, true);
            CopyToBuffer(buff, 0x05, matchID);
            CopyToBuffer(buff, 0x09, someOtherKey);
            CopyToBuffer(buff, 0x0D, uint10);
            CopyToBuffer(buff, 0x11, uint14);
            CopyToBuffer(buff, 0x15, LastDSLiveLogEventTime);
            CopyToBuffer(buff, 0x19, LastStatUpdateTime);
            CopyToBuffer(buff, 0x1D, playlistIndex);
            buff[0x21] = gameMode;
            buff[0x22] = byte25;
            buff[0x23] = byte26;
            buff[0x24] = byte27;
            buff[0x25] = byte28;
            buff[0x26] = byte29;
            buff[0x27] = byte2A;
            buff[0x28] = byte2B;
            buff[0x29] = byte2C;
            s.Write(buff, 0, 256);
        }
    }

    public class SessionInfosParameter
    {
        public SessionParameters sParams = new SessionParameters();
        public bool m_bSessionParametersAreSet;
        public SessionInfosParameter() 
        {
            //AF020A0E Data/99 - Standalone/Menu/TheProvingGrounds/Modelisation/World//
            //DE139C36 Data/- 14 - Maps GRO/03_MoscowUB_City/03_MoscowUB_City_LD/Modelisation/World//
            //9203DA88 Data/- 14 - Maps GRO/03_MoscowUB_City/03_MoscowUB_City_Global/Modelisation/World//
            //4E100B51 Data/99 - Standalone/Menu/GlobalGUI/Modelisation/World//
            //B2001CDC Data/99 - Standalone/Dedicated Server/Menu/Modelisation/World//
            sParams.byte3 = 1;
            sParams.mapKey = 0x9203DA88;
            sParams.matchID = 1;
            sParams.playlistIndex = 0;
            sParams.gameMode = 7;
            sParams.byte25 = 2;

            m_bSessionParametersAreSet = true;
        }

        public void toBuffer(Stream s)
        {
            sParams.toBuffer(s);
            s.WriteByte((byte)(m_bSessionParametersAreSet ? 1 : 0));
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
