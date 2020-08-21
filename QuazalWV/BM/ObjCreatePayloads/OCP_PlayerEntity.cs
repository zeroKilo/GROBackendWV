using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class OCP_PlayerEntity
    {
        public enum MoveMode
        {
            eMoveModeStop = 0,
            eMoveModeFree = 1,
            eMoveModeCover = 2,
            eMoveModeGoToPosition = 3,
            eMoveModeCross = 4,
            eMoveModeAnimControl = 5,
            eMoveModeSlide = 6,
            eMoveModeTeleport = 7,
        }
        //handle
        public uint handle;
        //replica data 1
        public byte unk1 = 0x56;
        public byte[] unk2 = new byte[4];
        public byte[] unk3 = new byte[4];
        //sub Stuff 1
        public MoveMode m_MoveMode = MoveMode.eMoveModeFree; 
        public ushort m_ADSDamage = 0;
        public ushort m_PostADSDamage = 0;
        public byte m_bIsInADSCone = 0;
        public byte m_BlitzShieldArmed = 0;
        public byte m_bHealthRegenActive = 1;
        //replica data 2
        public byte unk4 = 0x67;
        public byte[] unk5 = new byte[4];
        public byte[] unk6 = new byte[4];
        //sub Stuff 2
        //rest
        public byte playerLocalIndex = 0x0;
        public byte padID = 0x0;
        public byte teamID = 0x1;
        public byte classID = 0;
        public float Health1 = 100f;
        public float Health2 = 100f;
        public float DOB_Seconds = 0; 

        public OCP_PlayerEntity(uint h)
        {
            handle = h;
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            //Handle
            Helper.WriteU32LE(m, handle);

            //Replica Data 1
            Helper.WriteU8(m, (byte)unk2.Length);
            Helper.WriteU8(m, unk1);
            m.Write(unk2, 0, unk2.Length);
            //subStuff
            Helper.WriteFloatLE(m, 2); //m_ShootPosition
            Helper.WriteFloatLE(m, 3); //m_ShootTargetHandle
            Helper.WriteFloatLE(m, 4); //m_WhistlingBullet
            Helper.WriteU8(m, 5);  //m_Rush

            Helper.WriteU8(m, 6); //m_PlayerFire
            Helper.WriteU8(m, 7); //m_CurrentWeaponSlot
            Helper.WriteU8(m, 8); //m_WantedWeaponSlot
            Helper.WriteU8(m, 9); //m_OldWeaponSlot

            Helper.WriteU8(m, 0);//count,             m_ReplicatedCamPitch
            byte[] tmp = Helper.MakeFilledArray(0); //m_ReplicatedCamPitch
            m.Write(tmp, 0, tmp.Length);            //m_ReplicatedCamPitch

            Helper.WriteU8(m, (byte)m_MoveMode);
            Helper.WriteU32(m, 11); //???
            Helper.WriteU16(m, 12); //m_FocusedEntityReplication

            Helper.WriteU16(m, 13); //m_CoverHeight
            tmp = Helper.MakeFilledArray(9); //m_CoverFlagWanted + m_CoverNormal
            m.Write(tmp, 0, tmp.Length);     //m_CoverFlagWanted + m_CoverNormal
            Helper.WriteFloatLE(m, 1); //m_State
            Helper.WriteFloatLE(m, 1); //m_StateServer
            Helper.WriteU8(m, 16); //m_LaserSightStateCurr
            
            tmp = Helper.MakeFilledArray(12); //m_SlideVelocity
            m.Write(tmp, 0, tmp.Length);      //m_SlideVelocity
            
            Helper.WriteU8(m, 17); //m_SlideToRosaceAnim
            Helper.WriteU16(m, m_ADSDamage);
            Helper.WriteU16(m, m_PostADSDamage);
            Helper.WriteU8(m, m_bIsInADSCone);
            Helper.WriteU8(m, m_BlitzShieldArmed);
            tmp = Helper.MakeFilledArray(13); //m_OrderStatus
            m.Write(tmp, 0, tmp.Length);      //m_OrderStatus

            Helper.WriteU8(m, 22); //m_FireModeType
            Helper.WriteU8(m, 23); //m_RollAnimIndex
            tmp = Helper.MakeFilledArray(5); //m_ReplicatedPowerPC
            m.Write(tmp, 0, tmp.Length);     //m_ReplicatedPowerPC
            Helper.WriteU16(m, 24); //m_CurrentEnergyPC

            Helper.WriteU16(m, 25); //m_KikooMoveCount
            byte mood = 1;
            Helper.WriteU32LE(m, (uint)(1 << mood)); //m_Mood
            Helper.WriteU8(m, 27); //m_HitPart
            Helper.WriteU8(m, 28); //m_LeftHandSide
            Helper.WriteU8(m, m_bHealthRegenActive);

            //Replica Data 2
            Helper.WriteU8(m, (byte)unk5.Length);
            Helper.WriteU8(m, unk4);
            m.Write(unk2, 0, unk5.Length);
            //subStuff
            tmp = Helper.MakeFilledArray(8);
            m.Write(tmp, 0, tmp.Length);
            Helper.WriteU16(m, 3);
            tmp = Helper.MakeFilledArray(8);
            m.Write(tmp, 0, tmp.Length);

            Helper.WriteU8(m, 4);
            Helper.WriteU16(m, 5);

            //Rest
            Helper.WriteU8(m, teamID);
            Helper.WriteU8(m, classID);
            Helper.WriteFloatLE(m, Health1);
            Helper.WriteFloatLE(m, Health2);
            Helper.WriteU8(m, 0x22);
            Helper.WriteU8(m, 0x33);
            Helper.WriteU8(m, 0x44);
            for (int i = 0; i < 9; i++)
                Helper.WriteU8(m, 0);//count
            Helper.WriteFloatLE(m, DOB_Seconds);

            return m.ToArray();
        }
    }
}
