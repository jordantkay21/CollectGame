using System;
using System.Collections.Generic;
using UnityEngine;

namespace KayosStudios.InventorySystem.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] InventoryData inventoryData;
        [SerializeField] GameObject itemSlotPrefab;
        [SerializeField] Transform inventoryPanel;

        private Dictionary<ItemData, InventorySlotUI> activeSlots = new Dictionary<ItemData, InventorySlotUI>();

        private void OnEnable()
        {
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            PopulateUI();
        }

        private void OnDisable()
        {
            inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
        }

        private void PopulateUI()
        {
            foreach (var entry in inventoryData.items)
            {
                UpdateInventoryUI(entry.item, entry.count);
            }
        }

        private void UpdateInventoryUI(ItemData item, int count)
        {
            if (activeSlots.ContainsKey(item))
            {
                if (count > 0)
                {
                    activeSlots[item].ConfigureSlot(item.itemIcon,item.itemName, count);
                }
                else
                {
                    Destroy(activeSlots[item].gameObject);
                    activeSlots.Remove(item);
                }
            }
            else if (count > 0)
            {
                var slot = Instantiate(itemSlotPrefab, inventoryPanel).GetComponent<InventorySlotUI>();
                slot.ConfigureSlot(item.itemIcon,item.itemName, count);
                activeSlots[item] = slot;
            }
        }
    }
}