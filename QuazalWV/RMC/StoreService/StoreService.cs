using System.IO;
using QuazalWV.DB;

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
                case 17:
                    rmc.request = new RMCPacketRequestStoreService_InitiateBuyItem(s);
                    break;
                case 18:
                    rmc.request = new RMCPacketRequestStoreService_CompleteBuyItem(s);
                    break;
                case 20:
                    rmc.request = new RMCPacketRequestStoreService_InitiateBuyWeaponAndAttachComponents(s);
                    break;
                case 21:
                    rmc.request = new RMCPacketRequestStoreService_CompleteBuyWeaponAndAttachComponents(s);
                    break;
                case 26:
                    rmc.request = new RMCPacketRequestStoreService_InitiateBuyAbilityWithUpgrades(s);
                    break;
                case 27:
                    rmc.request = new RMCPacketRequestStoreService_CompleteBuyAbilityWithUpgrades(s);
                    break;
                case 30:
                    rmc.request = new RMCPacketRequestStoreService_InitiateBuyArmourAndAttachInserts(s);
                    break;
                case 31:
                    rmc.request = new RMCPacketRequestStoreService_CompleteBuyArmourAndAttachInserts(s);
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
                    reply = new RMCPResponseEmpty();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 11:
                    reply = new RMCPacketResponseStoreService_GetShoppingDetails();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 17:
                    var buyItemInitReq = (RMCPacketRequestStoreService_InitiateBuyItem)rmc.request;
                    trId = TransactionModel.SaveTransaction(
                        client.PID,
                        buyItemInitReq.CartItems[0].SkuId,
                        TransactionType.BuyItem,
                        buyItemInitReq.CartItems[0].VirtualCurrencyType
                    );
                    reply = new RMCPacketResponseStoreService_InitiateBuyItem(trId);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    // send complete transaction notif on success
                    if (trId > 0)
                        SendCompleteNotif(client, trId);
                    break;
                case 18:
                    var buyItemComplReq = (RMCPacketRequestStoreService_CompleteBuyItem)rmc.request;
                    TransactionModel.CompleteTransaction(buyItemComplReq.TransactionId);
                    reply = new RMCPacketResponseStoreService_CompleteBuyItem();
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
                        SendCompleteNotif(client, trId);
                    break;
                case 21:
                    var buyWeapComplReq = (RMCPacketRequestStoreService_CompleteBuyWeaponAndAttachComponents)rmc.request;
                    TransactionModel.CompleteTransaction(buyWeapComplReq.TransactionId);
                    reply = new RMCPacketResponseStoreService_CompleteBuyWeaponAndAttachComponents();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 26:
                    var buyAbilityInitReq = (RMCPacketRequestStoreService_InitiateBuyAbilityWithUpgrades)rmc.request;
                    trId = TransactionModel.SaveMultiItemTransaction(
                        client.PID,
                        buyAbilityInitReq.AbilitySkuData.SkuId,
                        TransactionType.BuyAbilityWithUpgrades,
                        buyAbilityInitReq.AbilitySkuData.VirtualCurrencyType,
                        buyAbilityInitReq.UpgradeSKUIdSlots
                    );
                    reply = new RMCPacketResponseStoreService_InitiateBuyAbilityWithUpgrades(trId);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    // send complete transaction notif on success
                    if (trId > 0)
                        SendCompleteNotif(client, trId);
                    break;
                case 27:
                    var buyAbilityComplReq = (RMCPacketRequestStoreService_CompleteBuyAbilityWithUpgrades)rmc.request;
                    TransactionModel.CompleteTransaction(buyAbilityComplReq.TransactionId);
                    reply = new RMCPacketResponseStoreService_CompleteBuyAbilityWithUpgrades();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                case 30:
                    var buyArmorWithInsertsInitReq = (RMCPacketRequestStoreService_InitiateBuyArmourAndAttachInserts)rmc.request;
                    trId = TransactionModel.SaveMultiItemTransaction(
                        client.PID,
                        buyArmorWithInsertsInitReq.ArmorSkuData.SkuId,
                        TransactionType.BuyArmourAndAttachInserts,
                        buyArmorWithInsertsInitReq.ArmorSkuData.VirtualCurrencyType,
                        buyArmorWithInsertsInitReq.InsertSKUIdSlots
                    );
                    reply = new RMCPacketResponseStoreService_InitiateBuyArmourAndAttachInserts(trId);
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    // send complete transaction notif on success
                    if (trId > 0)
                        SendCompleteNotif(client, trId);
                    break;
                case 31:
                    var buyArmorWithInsertsComplReq = (RMCPacketRequestStoreService_CompleteBuyArmourAndAttachInserts)rmc.request;
                    TransactionModel.CompleteTransaction(buyArmorWithInsertsComplReq.TransactionId);
                    reply = new RMCPacketResponseStoreService_CompleteBuyArmourAndAttachInserts();
                    RMC.SendResponseWithACK(client.udp, p, rmc, client, reply);
                    break;
                default:
                    Log.WriteLine(1, "[RMC Store] Error: Unknown Method 0x" + rmc.methodID.ToString("X"));
                    break;
            }
        }

        /// <summary>
        /// Signals transaction completion to the game to initiate a completion request.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="trId"></param>
        private static void SendCompleteNotif(ClientInfo client, uint trId)
        {
            NotificationQuene.AddNotification(new NotificationQueneEntry(client, 3000, 0, 1022, 1, trId, trId, 0, ""));
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
