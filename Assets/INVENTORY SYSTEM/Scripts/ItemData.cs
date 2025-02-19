using UnityEngine;

namespace KayosStudios.InventorySystem
{
    [CreateAssetMenu(fileName = "newItem", menuName = "KayosStudios/Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite itemIcon;
        public int maxStackSize;
    }
}