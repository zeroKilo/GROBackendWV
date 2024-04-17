using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuazalWV.DB;
using System.Drawing;

namespace QuazalWV
{
    public static class StoreService
    {
        public static void ProcessStoreServiceRequest(Stream s, RMCP rmc)
        {
            switch (rmc.methodID)
            {
                case 1:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 11:
                    break;
                case 20:
                    rmc.request = new RMCPacketRequestStoreService_InitiateBuyWeaponAndAttachComponents(s);
                    break;
                case 21:
                    rmc.request = new RMCPacketRequestStoreService_CompleteBuyWeaponAndAttachComponents(s);
                    break;
                default:
                    Log.WriteLine(1, "[RMC Store] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public static void HandleStoreServiceRequest(QPacket p, RMCP rmc, ClientInfo client)
        {
            RMCPResponse reply;
            uint trId = 0;
            switch (rmc.methodID)
            {
                case 1:
                    reply = new RMCPacketResponseStoreService_GetSKUs();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 8:
                    reply = new RMCPacketResponseStoreService_EnterCoupons();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 9:
                    //RemoveUnusedCoupons
                    reply = new RMCPResponseEmpty();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 11:
                    reply = new RMCPacketResponseStoreService_GetShoppingDetails();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 20:
                    var buyWeapInitReq = (RMCPacketRequestStoreService_InitiateBuyWeaponAndAttachComponents)rmc.request;
                    trId = TransactionModel.SaveTransaction(
                        client.PID, 
                        buyWeapInitReq.WeaponSkuData.SkuId,
                        TransactionType.BuyWeaponAndAttachComponents,
                        buyWeapInitReq.WeaponSkuData.VirtualCurrencyType
                    );
                    reply = new RMCPacketResponseStoreService_InitiateBuyWeaponAndAttachComponents(trId);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    // send complete transaction notif on success
                    if (trId > 0)
                        NotificationQuene.AddNotification(new NotificationQueneEntry(client, 3000, 0, 1022, 1, trId, trId, 0, ""));
                    break;
                case 21:
                    var buyWeapComplReq = (RMCPacketRequestStoreService_CompleteBuyWeaponAndAttachComponents)rmc.request;
                    TransactionModel.CompleteTransaction(buyWeapComplReq.TransactionId);
                    reply = new RMCPacketResponseStoreService_CompleteBuyWeaponAndAttachComponents();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC Store] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        public enum VirtualCurrencyType
        {
            RP = 1,
            GC = 2
        }

        /// <summary>
        /// The type of transaction request.
        /// </summary>
        /// <see cref="https://github.com/zeroKilo/GROBackendWV/wiki/RMC-Store-Service#transaction-type"/>
        public enum TransactionType
        {
            BuyItem = 0,
            BuyWeaponAndAttachComponents,
            BuyAndAttachComponents,
            BuyAndRepairItem,
            BuyAbilityWithUpgrades,
            BuyAndAttachUpgrades,
            BuyArmourAndAttachInserts,
            BuyAndAttachInserts
        }
    }
}
