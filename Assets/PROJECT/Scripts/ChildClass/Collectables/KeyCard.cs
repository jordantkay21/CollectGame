using UnityEngine;

namespace KayosStudios.TBD.Inventory.Collectible
{
    public class KeyCard : Collectible<KeyCard, object>
    {
        protected override object Collect()
        {
            DebugLogger.Log("KeyCard", $"Collected a new key card!");
            return null;
        }
    }
}
