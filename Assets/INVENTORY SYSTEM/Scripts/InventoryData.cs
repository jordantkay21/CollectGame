using System;
using System.Collections.Generic;
using UnityEngine;

namespace KayosStudios.InventorySystem
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "KayosStudios/Inventory/Inventory Data")]
    public class InventoryData : ScriptableObject
    {
        public List<ItemEntry> items = new List<ItemEntry>();

        [Serializable]
        public class ItemEntry
        {
            public ItemData item;
            public int count;
        }

        public event Action<ItemData, int> OnInventoryUpdated;

        public void ModifyItemCount(ItemData item, int amount)
        {
            ItemEntry entry = items.Find(i => i.item == item);

            if(entry == null)
            {
                entry = new ItemEntry { item = item, count = 0 };
                items.Add(entry);
            }

            entry.count += amount;

            if (entry.count < 0)
                entry.count = 0;

            OnInventoryUpdated?.Invoke(item, entry.count);
            DebugLogger.Log("InventorySystem", $"Inventory Updated: {item.itemName} = {entry.count}");
        }

        public bool CheckItemAvailability(ItemData item, int amount)
        {
            ItemEntry entry = items.Find(i => i.item == item);
            DebugLogger.Log("InventorySystem", $"Item Availability : {entry != null && entry.count >= amount}");
            return entry != null && entry.count >= amount;
        }
    }
}