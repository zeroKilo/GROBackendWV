﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class OCP_AbstractPlayerEntity
    {
        public uint handle;
        public byte unk1 = 0x55;
        public byte[] unk2 = new byte[4];
        public byte[] unk3 = new byte[4];
        public byte unk4 = 0x66;
        public byte[] unk5 = new byte[4];
        public byte[] unk6 = new byte[4];
        public byte playerLocalIndex = 0x0;
        public byte padID = 0x0;
        public byte teamID = 0x1;
        public uint rdvID = 0x1234;
        public uint unk11 = 0x88888888;

        public OCP_AbstractPlayerEntity(uint h)
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
            Helper.WriteU32(m, 1); //m_DeathCount
            Helper.WriteU32(m, 2); //m_AbilityInventoryId
            Helper.WriteU32(m, 3); //m_PassiveAbilityInventoryId
            Helper.WriteU32(m, 1000); //m_DesiredWeaponIds - main
            Helper.WriteU32(m, 4); //m_DesiredWeaponIds - pistol
            Helper.WriteU32(m, 4); //m_DesiredWeaponIds - grenade
            Helper.WriteU32(m, 5); //m_AchievementPoints
            Helper.WriteU32(m, 6); //m_HelmetInventoryId
            Helper.WriteU32(m, 7); //m_ArmorTierInventoryId
            Helper.WriteU16(m, 8); //m_SpawnBlockingReasons
            Helper.WriteU8(m, 0);  //m_Class
            Helper.WriteU16(m, 0); //m_ClassLevel
            Helper.WriteU32(m, 0); //m_PortraitId
            Helper.WriteU32(m, (uint)unk3.Length); //m_PersonaName
            m.Write(unk3, 0, unk3.Length);         //m_PersonaName

            //Replica Data 2
            Helper.WriteU8(m, (byte)unk5.Length);
            Helper.WriteU8(m, unk4);
            m.Write(unk2, 0, unk5.Length);
            //subStuff
            //Rest
            Helper.WriteU8(m, playerLocalIndex);
            Helper.WriteU8(m, padID);
            Helper.WriteU8(m, teamID);
            Helper.WriteU32LE(m, rdvID);
            Helper.WriteU32(m, unk11);
            return m.ToArray();
        }
    }
}
