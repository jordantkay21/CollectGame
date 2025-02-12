using UnityEngine;
using KayosStudios.TBD.Inventory;

public class CurrencyExchange : TradePost<CurrencyExchange>, IInteractable
{
    public void Interact()
    {
        TryTrade();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemToGive = ItemType.Key;
        amountToGive = 1;
        itemToTake = ItemType.Coin;
        amountToTake = 100;
    }

}
