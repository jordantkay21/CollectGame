using KayosStudios.TBD.CollectibleLogic;
using UnityEngine;

namespace KayosStudios.TBD.InventorySystem.Item
{
    public class Keycard : Collectible<Keycard, object>, IItem
    {
        private ItemType _itemType;

        public ItemType ItemType { get { return _itemType; } set { _itemType = value; } }

        public override void OnSpawn()
        {
            DebugLogger.Log("Keycard", $"Keycard spawned!");
        }

        protected override object Collect()
        {
            DebugLogger.Log("Keycard", $"Collected a new key card!");
            return null;
        }
    }
}