using UnityEngine;

namespace KayosStudios.TBD.Inventory.Collectible
{
    public class KeyCard : Collectible<KeyCard, object>
    {
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
