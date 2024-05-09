using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;

namespace QuazalWV.DB
{
    public static class TransactionModel
    {
        /// <summary>
        /// Persists a single-SKU transaction.
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="skuId"></param>
        /// <param name="trType"></param>
        /// <param name="currType"></param>
        /// <returns>New transaction ID or 0 on failure.</returns>
        public static uint SaveTransaction(uint pid, uint skuId, StoreService.TransactionType trType, StoreService.VirtualCurrencyType currType)
        {
            string currCol = currType == StoreService.VirtualCurrencyType.RP ? "IGCcost" : "GRcost";
            int totalCost = Convert.ToInt32(DBHelper.GetQueryResults($"SELECT {currCol} FROM skus WHERE iid={skuId}")[0][0] ?? "-1");
            if (totalCost < 0)
            {
                Log.WriteLine(1, $"[RMC Store] Transaction failed (non-existent skuId={skuId})", Color.Red);
                return 0;
            }
            
            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO transactions (initiatedAt,pid,skuId,transactionType,currencyType,totalPrice) " +
                $"VALUES ({now},{pid},{skuId},{(uint)trType},{(uint)currType},{totalCost});" +
                $"SELECT last_insert_rowid();", DBHelper.connection);
            try
            {
                return (uint)(long)cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                Log.WriteLine(1, e.ToString(), Color.Red);
                Log.WriteLine(1, $"[RMC Store] Transaction failed (pid={pid}, skuId={skuId})", Color.Red);
                return 0;
            }
        }

        /// <summary>
        /// Persists a multi-SKU transaction.
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="skuId"></param>
        /// <param name="trType"></param>
        /// <param name="currType"></param>
        /// <returns></returns>
        public static uint SaveMultiItemTransaction(uint pid, uint skuId, StoreService.TransactionType trType, StoreService.VirtualCurrencyType currType, List<GR5_IdSlotPair> extraItems)
        {
            string currCol = currType == StoreService.VirtualCurrencyType.RP ? "IGCcost" : "GRcost";
            int totalCost = Convert.ToInt32(DBHelper.GetQueryResults($"SELECT {currCol} FROM skus WHERE iid={skuId}")[0][0] ?? "-1");
            if (totalCost < 0)
            {
                Log.WriteLine(1, $"[RMC Store] Transaction failed (non-existent skuId={skuId})", Color.Red);
                return 0;
            }

            foreach(GR5_IdSlotPair pair in extraItems)
            {
                // querying to be optimized in future
                var extraItemCost = Convert.ToInt32(DBHelper.GetQueryResults($"SELECT {currCol} FROM skus WHERE iid={pair.Id}")[0][0] ?? "-1");
                if(extraItemCost < 0)
                {
                    Log.WriteLine(1, $"[RMC Store] Transaction failed (non-existent skuId={pair.Id})", Color.Red);
                    return 0;
                }
                totalCost += extraItemCost;
            }

            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO transactions (initiatedAt,pid,skuId,transactionType,currencyType,totalPrice) " +
                $"VALUES ({now},{pid},{skuId},{(uint)trType},{(uint)currType},{totalCost});" +
                $"SELECT last_insert_rowid();", DBHelper.connection);
            try
            {
                return (uint)(long)cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                Log.WriteLine(1, e.ToString(), Color.Red);
                Log.WriteLine(1, $"[RMC Store] Transaction failed (pid={pid}, skuId={skuId})", Color.Red);
                return 0;
            }
        }

        public static bool CompleteTransaction(uint transactionId)
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE transactions SET completedAt={now} WHERE id={transactionId}", DBHelper.connection);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
