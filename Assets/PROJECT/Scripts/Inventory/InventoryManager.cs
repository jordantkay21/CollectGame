using KayosStudios.TBD.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KayosStudios.TBD.Inventory
{
    public enum ItemType
    {
        Coin,
        Key
    }
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        private Dictionary<ItemType, int> itemInventory = new Dictionary<ItemType, int>();
        [SerializeField] int startingCoins = 0;
        [SerializeField] int startingKeys = 0;

        public int GetCoinCount => itemInventory[ItemType.Coin];
        public int GetKeyCount => itemInventory[ItemType.Key];

        private event Action<ItemType, int> OnInventoryChange;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            itemInventory[ItemType.Coin] = startingCoins;
            itemInventory[ItemType.Key] = startingKeys;
        }

        private void OnEnable()
        {
            //Subscribe to collectible events
            Coin.OnCollected += (amount) => ModifyItemCount(ItemType.Coin, amount);
            Key.OnCollected += (_) => ModifyItemCount(ItemType.Key, 1);

            CurrencyExchange.OnTradeSuccess += (item, amount) => ModifyItemCount(item, amount);
            CurrencyExchange.CanAfford += CheckCount;
        }

        private void OnDisable()
        {
            //Unsubscribe to collectible events
            Coin.OnCollected -= (amount) => ModifyItemCount(ItemType.Coin, amount);
            Key.OnCollected -= (_) => ModifyItemCount(ItemType.Key, 1);
            CurrencyExchange.CanAfford -= CheckCount;
        }

        private bool CheckCount(ItemType item, int amount)
        {
            if (itemInventory[item] >= amount)
            {
                DebugLogger.Log("Inventory", $"Player has enough for the proposed Trade!", DebugLevel.Verbose);
                return true;
            }

            DebugLogger.Log("Inventory", $"Player does not have enough for the proposed Trade!", DebugLevel.Verbose);
            return false;
        }

        public void ModifyItemCount(ItemType item, int amount)
        {
            if (!itemInventory.ContainsKey(item))
            {
                itemInventory[item] = 0;
            }

            itemInventory[item] += amount;

            //Ensure we don't allow negative item counts
            if(itemInventory[item] < 0)
            {
                itemInventory[item] = 0;
            }

            OnInventoryChange?.Invoke(item, itemInventory[item]);
            DebugLogger.Log("Inventory", $"Inventory Updated: {item} = {itemInventory[item]}");
        }
    }
}