using KayosStudios.TBD.InteractionSystem;
using KayosStudios.TBD.InventorySystem.Item;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KayosStudios.TBD.InventorySystem
{
    public enum ItemType
    {
        Keycard
    }

    public interface IItem
    {
        ItemType ItemType { get; set; }
    }
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] int startingKeycards = 0;

        private Dictionary<ItemType, int> itemInventory = new Dictionary<ItemType, int>();

        public static event Action<ItemType, int> OnInventoryChange;

        private void Awake()
        {
            ModifyItemCount(ItemType.Keycard, startingKeycards);
        }

        private void OnEnable()
        {
            Keycard.OnCollection += (_) => ModifyItemCount(ItemType.Keycard, 1);
            Interactable.OnTransactionInitiation += (item, amount) => ModifyItemCount(item, amount);
            Interactable.CanAfford += CheckInventory;
        }

        private bool CheckInventory(ItemType item, int amount)
        {
            if (itemInventory[item] >= amount)
            {
                DebugLogger.Log("InventoryManager", $"Player has enough for the proposed transaction");
                return true;
            }
            else
            {
                DebugLogger.Log("InventoryManager", $"Player does not have the {amount} {item}(s) needed for the proposed transaction.");
                return false;
            }
        }

        private void OnDisable()
        {
            Keycard.OnCollection -= (_) => ModifyItemCount(ItemType.Keycard, 1);
        }


        private void ModifyItemCount(ItemType item, int amount)
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
            DebugLogger.Log("InventoryManager", $"Inventory Updated: {item} = {itemInventory[item]}");
        }
    }
}