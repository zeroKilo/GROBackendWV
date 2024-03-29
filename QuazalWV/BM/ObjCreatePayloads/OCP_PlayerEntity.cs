﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Runtime.Remoting;

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

        public enum ClassInfoMemBuffer
        {
            eMainWeapon = 0,
            ePistol = 1,
            eGrenade = 2,
            eArmor = 3,
            eHelmetKey = 4,
            eAbility = 5,
            ePassiveAbility = 6,
            eWeaponBoost = 7,
            eBody = 8
        }

        //handle
        public uint handle;
        //replica data 1
        public byte unk1 = 0x56;
        public byte[] unk2 = new byte[4];
        public byte[] unk3 = new byte[4];
        //sub Stuff 1
        public byte m_Rush = 0;
        public MoveMode m_MoveMode = MoveMode.eMoveModeFree; 
        public ushort m_ADSDamage = 0;
        public ushort m_PostADSDamage = 0;
        public byte m_bIsInADSCone = 0;
        public byte m_BlitzShieldArmed = 1;
        public uint m_Mood = 0xCE;//cool + instinct + precise + standlow + standverylow
        public byte m_bHealthRegenActive = 1;
        //replica data 2
        public byte unk4 = 0x67;
        public byte[] unk5 = new byte[4];
        public byte[] unk6 = new byte[4];
        //sub Stuff 2
        //rest
        public byte playerLocalIndex = 0x0; //unused
        public byte padID = 0x0; //unused
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
            Helper.WriteU8(m, 5);  

            Helper.WriteU8(m, m_Rush);
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
            Helper.WriteU32LE(m, m_Mood);
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
            Helper.WriteU8(m, 0x2F);
            Helper.WriteU8(m, 0x3F);
            Helper.WriteU8(m, 0x4F);
            byte[] buffer;
            // class info memBuffers
                for (int i = 0; i < 9; i++)
                {
                    switch ((ClassInfoMemBuffer)i)
                    {
                        case ClassInfoMemBuffer.eMainWeapon:

                        ClassInfo_Gun mainRifleInfo = new ClassInfo_Gun(30583);//asval
                        int mainRifInfoSize = mainRifleInfo.MakePayload().Length - 1;
                        mainRifleInfo.memBufferSize = Convert.ToByte(mainRifInfoSize);
                        buffer = mainRifleInfo.MakePayload();
                        m.Write(buffer, 0, buffer.Length);
                            break;

                        case ClassInfoMemBuffer.ePistol:

                        ClassInfo_Gun pistolInfo = new ClassInfo_Gun(30583);//asval
                        int pistolInfoSize = pistolInfo.MakePayload().Length - 1;
                        pistolInfo.memBufferSize = Convert.ToByte(pistolInfoSize);
                        buffer = pistolInfo.MakePayload();
                        m.Write(buffer, 0, buffer.Length);
                            break;

                        case ClassInfoMemBuffer.eGrenade:

                        ClassInfo_Grenade nadeInfo = new ClassInfo_Grenade(30583);//asval
                        int nadeInfoSize = nadeInfo.MakePayload().Length - 1;
                        nadeInfo.memBufferSize = Convert.ToByte(nadeInfoSize);
                        buffer = nadeInfo.MakePayload();
                        m.Write(buffer, 0, buffer.Length);
                            break;

                        case ClassInfoMemBuffer.eArmor:

                        ClassInfo_Armor armorInfo = new ClassInfo_Armor();
                        int armorInfoSize = armorInfo.MakePayload().Length - 1;
                        armorInfo.memBufferSize = Convert.ToByte(armorInfoSize);
                        buffer = armorInfo.MakePayload();
                        m.Write(buffer, 0, buffer.Length);
                            break;

                        case ClassInfoMemBuffer.eHelmetKey:

                            Helper.WriteU8(m, 4);//buffer size
                            Helper.WriteU32LE(m, 0xF8700A85);//GRO-COM1-Helm00.gao
                            break;

                        case ClassInfoMemBuffer.eAbility:

                            ClassInfo_Ability abilityInfo = new ClassInfo_Ability(6);
                            int abInfoSize = abilityInfo.MakePayload().Length - 1;
                            abilityInfo.memBufferSize = Convert.ToByte(abInfoSize);
                            buffer = abilityInfo.MakePayload();//Blitz
                            m.Write(buffer, 0, buffer.Length);
                            break;

                        case ClassInfoMemBuffer.ePassiveAbility:

                            ClassInfo_PassiveAbility pasAbilityInfo = new ClassInfo_PassiveAbility(3);
                            int pasAbInfoSize = pasAbilityInfo.MakePayload().Length - 1;
                            pasAbilityInfo.memBufferSize = Convert.ToByte(pasAbInfoSize);
                            buffer = pasAbilityInfo.MakePayload();//Harden
                            m.Write(buffer, 0, buffer.Length);
                            break;

                        case ClassInfoMemBuffer.eWeaponBoost:

                            buffer = new ClassInfo_Boost().MakePayload();//const size
                            m.Write(buffer, 0, buffer.Length);
                            break;

                        case ClassInfoMemBuffer.eBody:

                            buffer = new ClassInfo_Body().MakePayload();//const size
                            m.Write(buffer, 0, buffer.Length);
                            break;
                    }
                }
            Helper.WriteFloatLE(m, DOB_Seconds);
            return m.ToArray();
        }
    }
}
