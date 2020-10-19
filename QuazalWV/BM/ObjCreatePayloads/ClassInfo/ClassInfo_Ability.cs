using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class ClassInfo_Ability
    {
        //Base
        enum ePowerModifiableType
        {
            ActivationEnergy_F = 0,
            StillBurnRate_F = 1,
            MoveBurnRate_F = 2,
            RechargeRate_F = 3,
            CooldownDuration_F = 4,
            EnergyAtCooldownEnd_F = 5,
            EnergyPool_F = 6
        }

        //Oracle/HBS
        enum ePowerHBSModifiableType
        {
            ActivationEnergyMultiplier_F = 0,
            StillBurnRateMultiplier_F = 1,
            MoveBurnRateMultiplier_F = 2,
            WaveLifeSpan_F = 3,
            WaveMaxDist_F = 4,
            WaveWidthStart_F = 5,
            SpotDuration_F = 6,
            HeartRange_F = 7,
            LongerSpotDurationChance_F = 8,
            SpotDelta_F = 9,
            WaveLengthStart_F = 10,
            WaveHeight_F = 11,
            WaveAlpha_F = 12,
            WaveWidthEnd_F = 13,
            WaveLengthEnd_F = 14,
        }

        //Cloak
        enum ePowerCLKModifiableType
        {
            ActivationEnergyMultiplier_F = 0,
            StillBurnRateMultiplier_F = 1,
            MoveBurnRateMultiplier_F = 2,
            EnergyLossPerPercentHealth_F = 3,
            StayStealthTestPeriod_F = 4,
            StealthRemainChance_F = 5,
            WeaponFireBurnRate_F = 6,
            StealthOpacity_F = 7,
            TimeToMaxOpacity_F = 8,
            OpacityWithinVisibleRadius_F = 9,
            VisibleRadius_F = 10,
            MaxOpacityChangePerSec_F = 11,
            BlockSize_F = 12,
            PatchFactor_F = 13
        }

        //Aegis
        enum ePowerAegisModifiableType
        {
            ActivationEnergyMultiplier_F = 0,
            StillBurnRateMultiplier_F = 1,
            MoveBurnRateMultiplier_F = 2,
            ShieldRadius_F = 3,
            DeflectionAngleMin_F = 4,
            DeflectionAngleMax_F = 5,
            DamageRatio_F = 6,
            EnergyDeflectRatio_F = 7,
            EnergyChargeDuration_F = 8,
            SecondaryDeflectAngle_F = 9,
            DeflectAwayChance_F = 10,
            MaxOverchargeDuration_F = 11
        }

        //EMP
        enum ePowerBlackoutModifiableType
        {
            ActivationEnergyMultiplier_F = 0,
            StillBurnRateMultiplier_F = 1,
            MoveBurnRateMultiplier_F = 2,
            MaxOverChargeDuration_F = 3,
            EnergyDepleteBaseRatio_F = 4,
            RadiusAtFullCharge_F = 5,
            ChargeRatioMultiplier_F = 6,
            MaxEnergyToDeplete_F = 7,
            EnergyDepleteRatioMultiplier_F = 8,
            PropagationSpeed_F = 9,
            ResidualEffectDuration_F = 10,
            ChanceToDisableElectronics_F = 11,
            EnergyGainRateWhileCharging_F = 12,
            FiringDisabledDuration_F = 13
        }

        //ADS/Heat
        enum ePowerADSModifiableType
        {
            ActivationEnergyMultiplier_F = 0,
            StillBurnRateMultiplier_F = 1,
            MoveBurnRateMultiplier_F = 2,
            ConeApexForwardOffset_F = 3,
            ConeApexVerticalOffset_F = 4,
            ConeApexSideOffset_F = 5,
            BaseDamageDPS_F = 6,
            BurnTimeScale_F = 7,
            BurnDistanceScale_F = 8,
            ResidualBurnTimeScale_F = 9,
            ResidualBurnDuration_F = 10,
            Reticule_F = 11,
            ConeLength_F = 12
        }

        //Blitz
        enum ePowerBlitzModifiableType
        {
            ActivationEnergyMultiplier_F = 0,
            StillBurnRateMultiplier_F = 1,
            MoveBurnRateMultiplier_F = 2,
            RushStretch_F = 3,
            SensitivityScale_F = 4,
            MaxAimTurnSpeed_F = 5,
            ShieldMoveAnimationTime_F = 6,
            ShieldAngle_F = 7,
            ShieldRadius_F = 8,
            ShieldHeight_F = 9,
            OffsetToShieldOriginX_F = 10,
            OffsetToShieldOriginY_F = 11,
            OffsetToShieldOriginZ_F = 12,
            KODuration_F = 13
        }

        public byte memBufferSize;
        byte abilityId;
        float energyPool;
        float rechargeRate;
        byte upgradeSlot1;
        byte upgradeSlot2;
        byte upgradeSlot3;//12B

        // Base ability modifiers (mod list 3)
        const byte nbBaseModifiers = 7;
        const byte baseModBitmask = 0xFF;
        List<float> baseModifiers;//42B
        // Ability-specific modifiers (mod lists 4-9)
        byte nbSpecificModifiers;
        const ushort specificModBitmask = 0xFFFF;//45B
        List<float> specificModifiers;//56 + 45 = 101B

        public ClassInfo_Ability(byte ability)
        {
            memBufferSize = 0; //set externally
            abilityId = ability;
            energyPool = 100f;
            rechargeRate = 3f;
            upgradeSlot1 = 1;
            upgradeSlot2 = 2;
            upgradeSlot3 = 3;

            baseModifiers = new List<float>();
            specificModifiers = new List<float>();

            for (byte b = 0; b < nbBaseModifiers; b++)
            {
                switch((ePowerModifiableType)b)
                {
                    case ePowerModifiableType.ActivationEnergy_F:
                        baseModifiers.Add(40.0f);
                        break;
                    case ePowerModifiableType.StillBurnRate_F:
                        baseModifiers.Add(5.0f);
                        break;
                    case ePowerModifiableType.MoveBurnRate_F:
                        baseModifiers.Add(6.0f);
                        break;
                    case ePowerModifiableType.RechargeRate_F:
                        baseModifiers.Add(3.0f);
                        break;
                    case ePowerModifiableType.CooldownDuration_F:
                        baseModifiers.Add(30.0f);
                        break;
                    case ePowerModifiableType.EnergyAtCooldownEnd_F:
                        baseModifiers.Add(90.0f);
                        break;
                    case ePowerModifiableType.EnergyPool_F:
                        baseModifiers.Add(100.0f);
                        break;
                }
            }

            switch(abilityId)
            {
                case 1://Oracle/HBS
                    nbSpecificModifiers = 15;
                    for(byte b = 0; b < nbSpecificModifiers ;b++)
                    {
                        switch ((ePowerHBSModifiableType)b)
                        {
                            case ePowerHBSModifiableType.ActivationEnergyMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerHBSModifiableType.StillBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerHBSModifiableType.MoveBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerHBSModifiableType.WaveLifeSpan_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerHBSModifiableType.WaveMaxDist_F:
                                specificModifiers.Add(100.0f);
                                break;
                            case ePowerHBSModifiableType.WaveWidthStart_F:
                                specificModifiers.Add(10.0f);
                                break;
                            case ePowerHBSModifiableType.SpotDuration_F:
                                specificModifiers.Add(10.0f);
                                break;
                            case ePowerHBSModifiableType.HeartRange_F:
                                specificModifiers.Add(30.0f);
                                break;
                            case ePowerHBSModifiableType.LongerSpotDurationChance_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerHBSModifiableType.SpotDelta_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerHBSModifiableType.WaveLengthStart_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerHBSModifiableType.WaveHeight_F:
                                specificModifiers.Add(30.0f);
                                break;
                            case ePowerHBSModifiableType.WaveAlpha_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerHBSModifiableType.WaveWidthEnd_F:
                                specificModifiers.Add(2.0f);
                                break;
                            case ePowerHBSModifiableType.WaveLengthEnd_F:
                                specificModifiers.Add(3.0f);
                                break;
                        }
                    }
                    break;
                case 2://Cloak
                    nbSpecificModifiers = 14;
                    for(byte b = 0; b < nbSpecificModifiers; b++)
                    {
                        switch((ePowerCLKModifiableType)b)
                        {
                            case ePowerCLKModifiableType.ActivationEnergyMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerCLKModifiableType.StillBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerCLKModifiableType.MoveBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerCLKModifiableType.EnergyLossPerPercentHealth_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerCLKModifiableType.StayStealthTestPeriod_F:
                                specificModifiers.Add(5.0f);
                                break;
                            case ePowerCLKModifiableType.StealthRemainChance_F:
                                specificModifiers.Add(50.0f);
                                break;
                            case ePowerCLKModifiableType.WeaponFireBurnRate_F:
                                specificModifiers.Add(5.0f);
                                break;
                            case ePowerCLKModifiableType.StealthOpacity_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerCLKModifiableType.TimeToMaxOpacity_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerCLKModifiableType.OpacityWithinVisibleRadius_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerCLKModifiableType.VisibleRadius_F:
                                specificModifiers.Add(50.0f);
                                break;
                            case ePowerCLKModifiableType.MaxOpacityChangePerSec_F:
                                specificModifiers.Add(5.0f);
                                break;
                            case ePowerCLKModifiableType.BlockSize_F:
                                specificModifiers.Add(10.0f);
                                break;
                            case ePowerCLKModifiableType.PatchFactor_F:
                                specificModifiers.Add(1.0f);
                                break;
                        }
                    }
                    break;
                case 3://Aegis
                    nbSpecificModifiers = 12;
                    for (byte b = 0; b < nbSpecificModifiers; b++)
                    {
                        switch ((ePowerAegisModifiableType)b)
                        {
                            case ePowerAegisModifiableType.ActivationEnergyMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerAegisModifiableType.StillBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerAegisModifiableType.MoveBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerAegisModifiableType.ShieldRadius_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerAegisModifiableType.DeflectionAngleMin_F:
                                specificModifiers.Add(0.0f);
                                break;
                            case ePowerAegisModifiableType.DeflectionAngleMax_F:
                                specificModifiers.Add(165.0f);
                                break;
                            case ePowerAegisModifiableType.DamageRatio_F:
                                specificModifiers.Add(1.5f);
                                break;
                            case ePowerAegisModifiableType.EnergyDeflectRatio_F:
                                specificModifiers.Add(2.0f);
                                break;
                            case ePowerAegisModifiableType.EnergyChargeDuration_F:
                                specificModifiers.Add(10.0f);
                                break;
                            case ePowerAegisModifiableType.SecondaryDeflectAngle_F:
                                specificModifiers.Add(25.0f);
                                break;
                            case ePowerAegisModifiableType.DeflectAwayChance_F:
                                specificModifiers.Add(50.0f);
                                break;
                            case ePowerAegisModifiableType.MaxOverchargeDuration_F:
                                specificModifiers.Add(5.0f);
                                break;
                        }
                    }
                    break;
                case 4://EMP/Blackout
                    nbSpecificModifiers = 14;
                    for (byte b = 0; b < nbSpecificModifiers; b++)
                    {
                        switch ((ePowerBlackoutModifiableType)b)
                        {
                            case ePowerBlackoutModifiableType.ActivationEnergyMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlackoutModifiableType.StillBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlackoutModifiableType.MoveBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlackoutModifiableType.MaxOverChargeDuration_F:
                                specificModifiers.Add(5.0f);
                                break;
                            case ePowerBlackoutModifiableType.EnergyDepleteBaseRatio_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlackoutModifiableType.RadiusAtFullCharge_F:
                                specificModifiers.Add(50.0f);
                                break;
                            case ePowerBlackoutModifiableType.ChargeRatioMultiplier_F:
                                specificModifiers.Add(1.5f);
                                break;
                            case ePowerBlackoutModifiableType.MaxEnergyToDeplete_F:
                                specificModifiers.Add(40.0f);
                                break;
                            case ePowerBlackoutModifiableType.EnergyDepleteRatioMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlackoutModifiableType.PropagationSpeed_F:
                                specificModifiers.Add(100.0f);
                                break;
                            case ePowerBlackoutModifiableType.ResidualEffectDuration_F:
                                specificModifiers.Add(10.0f);
                                break;
                            case ePowerBlackoutModifiableType.ChanceToDisableElectronics_F:
                                specificModifiers.Add(50.0f);
                                break;
                            case ePowerBlackoutModifiableType.EnergyGainRateWhileCharging_F:
                                specificModifiers.Add(1.5f);
                                break;
                            case ePowerBlackoutModifiableType.FiringDisabledDuration_F:
                                specificModifiers.Add(10.0f);
                                break;
                        }
                    }
                    break;
                case 5://ADS/Heat
                    nbSpecificModifiers = 13;
                    for (byte b = 0; b < nbSpecificModifiers; b++)
                    {
                        switch ((ePowerADSModifiableType)b)
                        {
                            case ePowerADSModifiableType.ActivationEnergyMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerADSModifiableType.StillBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerADSModifiableType.MoveBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerADSModifiableType.ConeApexForwardOffset_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerADSModifiableType.ConeApexVerticalOffset_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerADSModifiableType.ConeApexSideOffset_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerADSModifiableType.BaseDamageDPS_F:
                                specificModifiers.Add(5.0f);
                                break;
                            case ePowerADSModifiableType.BurnTimeScale_F:
                                specificModifiers.Add(2.0f);
                                break;
                            case ePowerADSModifiableType.BurnDistanceScale_F:
                                specificModifiers.Add(1.5f);
                                break;
                            case ePowerADSModifiableType.ResidualBurnTimeScale_F:
                                specificModifiers.Add(1.5f);
                                break;
                            case ePowerADSModifiableType.ResidualBurnDuration_F:
                                specificModifiers.Add(5.0f);
                                break;
                            case ePowerADSModifiableType.Reticule_F:
                                specificModifiers.Add(2.0f);
                                break;
                            case ePowerADSModifiableType.ConeLength_F:
                                specificModifiers.Add(80.0f);
                                break;
                        }
                    }
                    break;
                case 6://Blitz
                    nbSpecificModifiers = 14;
                    for (byte b = 0; b < nbSpecificModifiers; b++)
                    {
                        switch ((ePowerBlitzModifiableType)b)
                        {
                            case ePowerBlitzModifiableType.ActivationEnergyMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlitzModifiableType.StillBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlitzModifiableType.MoveBurnRateMultiplier_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlitzModifiableType.RushStretch_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlitzModifiableType.SensitivityScale_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlitzModifiableType.MaxAimTurnSpeed_F:
                                specificModifiers.Add(20.0f);
                                break;
                            case ePowerBlitzModifiableType.ShieldMoveAnimationTime_F:
                                specificModifiers.Add(0.5f);
                                break;
                            case ePowerBlitzModifiableType.ShieldAngle_F:
                                specificModifiers.Add(25.0f);
                                break;
                            case ePowerBlitzModifiableType.ShieldRadius_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlitzModifiableType.ShieldHeight_F:
                                specificModifiers.Add(1.5f);
                                break;
                            case ePowerBlitzModifiableType.OffsetToShieldOriginX_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlitzModifiableType.OffsetToShieldOriginY_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlitzModifiableType.OffsetToShieldOriginZ_F:
                                specificModifiers.Add(1.0f);
                                break;
                            case ePowerBlitzModifiableType.KODuration_F:
                                specificModifiers.Add(10.0f);
                                break;
                        }
                    }
                    break;
            }
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, memBufferSize);
            Helper.WriteU8(m, abilityId);
            Helper.WriteFloatLE(m, energyPool);
            Helper.WriteFloatLE(m, rechargeRate);
            Helper.WriteU8(m, upgradeSlot1);
            Helper.WriteU8(m, upgradeSlot2);
            Helper.WriteU8(m, upgradeSlot3);

            //base modifiers list
            Helper.WriteU8(m, nbBaseModifiers);
            Helper.WriteU8(m, baseModBitmask);
            foreach (float modifier in baseModifiers)
                Helper.WriteFloat(m, modifier);

            //specific modifiers list
            Helper.WriteU8(m, nbSpecificModifiers);
            Helper.WriteU16(m, specificModBitmask);
            foreach (float modifier in specificModifiers)
                Helper.WriteFloat(m, modifier);
            return m.ToArray();//101B (Blitz)
        }
    }
}
