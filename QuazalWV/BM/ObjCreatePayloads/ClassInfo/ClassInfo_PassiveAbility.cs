using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuazalWV
{
    public class ClassInfo_PassiveAbility
    {
        public byte memBufferSize;
        byte passiveAbilityId;

        //base passive ability modifiers
        const byte nbBaseModifiers = 2;
        const byte baseModBitmask = 0xFF;
        float teamSharingRadius;
        float partySharingRadius;

        //specific passive ability modifiers
        byte nbSpecificModifiers;
        const byte specificModBitmask = 0xFF;

        public ClassInfo_PassiveAbility(byte passiveAbilityId)
        {
            this.passiveAbilityId = passiveAbilityId;
            teamSharingRadius = 50f;
            partySharingRadius = 40f;
            switch(passiveAbilityId)
            {
                case 0:
                    memBufferSize = 21;
                    nbSpecificModifiers = 2;
                    break;
                default:
                    memBufferSize = 17;
                    nbSpecificModifiers = 1;
                    break;
            }
        }

        public byte[] MakePayload()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU8(m, memBufferSize);
            Helper.WriteU8(m, passiveAbilityId);

            //base passive ability modifier list (14)
            Helper.WriteU8(m, nbBaseModifiers);
            Helper.WriteU8(m, baseModBitmask);
            Helper.WriteFloat(m, teamSharingRadius);
            Helper.WriteFloat(m, partySharingRadius);
            //specific passive ability modifier list (15-20)
            Helper.WriteU8(m, nbSpecificModifiers);
            Helper.WriteU8(m, specificModBitmask);
            switch(passiveAbilityId)
            {
                //eAmmoSupplierModifiable
                case 0:
                    float ammoRegenInterval = 5f;
                    float ammoRegenPercentage = 35f;
                    Helper.WriteFloat(m, ammoRegenInterval);
                    Helper.WriteFloat(m, ammoRegenPercentage);
                    break;
                //eEnergySupplierModifiable
                case 1:
                    float energyRegenRate = 5f;
                    Helper.WriteFloat(m, energyRegenRate);
                    break;
                //eShootDetectionModifiable
                case 2:
                    float shootDetectionRadius = 50f;
                    Helper.WriteFloat(m, shootDetectionRadius);
                    break;
                //eHardenModifiable
                case 3:
                    float armorBoostRate = 15f;
                    Helper.WriteFloat(m, armorBoostRate);
                    break;
                //eHealthRegenModifiable
                case 4:
                    float healthRegenRate = 5f;
                    Helper.WriteFloat(m, healthRegenRate);
                    break;
                //eMoveDetectionModifiable
                case 5:
                    float moveDetectionRadius = 40f;
                    Helper.WriteFloat(m, moveDetectionRadius);
                    break;
            }
            return m.ToArray();
        }
    }
}
