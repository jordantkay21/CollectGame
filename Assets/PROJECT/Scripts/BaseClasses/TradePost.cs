using KayosStudios.TBD.Inventory;
using System;
using UnityEngine;


public abstract class TradePost<T> : MonoBehaviour where T:TradePost<T>
{
    [Header("Reward Settings")]
    [SerializeField] protected ItemType itemToGive;
    [SerializeField] protected int amountToGive;

    [Header("Price Settings")]
    [SerializeField] protected ItemType itemToTake;
    [SerializeField] protected int amountToTake;

    //Delegates for checking and modifying inventory
    public static Func<ItemType, int, bool> CanAfford;
    public static Action<ItemType, int> OnTradeSuccess;
    public static Action OnTradeFailure;
    public static Action<string> OnEnterTradingRange;
    public static Action<string> OnExitTradingRange;


    [ContextMenu("TryTrade")]
    public void TryTrade()
    {
        if (CanAfford != null) //Ensures there is a subscriber
        {
            bool canAfford = CanAfford.Invoke(itemToTake, amountToTake);

            if (canAfford)
            {
                DebugLogger.Log("Trader", $"Trade has been successful! {amountToTake} {itemToTake}(s) has been traded for {amountToGive} {itemToGive}(s).", DebugLevel.Verbose);
                OnTradeSuccess?.Invoke(itemToTake, -amountToTake);
                OnTradeSuccess?.Invoke(itemToGive, amountToGive);
            }
            else
            {
                DebugLogger.Log("Trader", $"Trade has failed. Player Lacks required items ({amountToTake} of {itemToTake}) to trade for {amountToGive} {itemToGive}(s).", DebugLevel.Verbose);
                OnTradeFailure?.Invoke();
            }
        }
        else
        {
            DebugLogger.Log("Trader", $"No inventory system subscribed to Trade Post.", DebugLevel.Warning);
        }
    }
}
