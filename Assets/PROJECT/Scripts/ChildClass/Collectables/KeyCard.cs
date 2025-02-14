using UnityEngine;

namespace KayosStudios.TBD.Inventory.Collectible
{
    public class KeyCard : Collectible<KeyCard, object>, IItem
    {
        [SerializeField] ItemType itemType;
        
        public ItemType ItemType { get { return itemType; } set { itemType = value; } }

        public override void OnSpawn()
        {
            DebugLogger.Log("KeyCard", $"KeyCard spawned!");
        }

        protected override object Collect()
        {
            DebugLogger.Log("KeyCard", $"Collected a new key card!");
            return null;
        }
    }
}
