using KayosStudios.TBD.Inventory.Collectible;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KayosStudios.TBD.Inventory
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
        public static InventoryManager Instance;

        [SerializeField] int startingKeycards = 0;

        private Dictionary<ItemType, int> itemInventory = new Dictionary<ItemType, int>();

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

            itemInventory[ItemType.Keycard] = startingKeycards;
        }

        private void OnEnable()
        {
            KeyCard.OnCollection += (_) => ModifyItemCount(ItemType.Keycard, 1);
        }

        private void OnDisable()
        {
            KeyCard.OnCollection -= (_) => ModifyItemCount(ItemType.Keycard, 1);
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
            DebugLogger.Log("Inventory", $"Inventory Updated: {item} = {itemInventory[item]}");
        }
    }
}