using KayosStudios.TBD.BaseClass;
using UnityEngine;

namespace KayosStudios.TBD.Inventory.Items
{
    public class Key : Collectible<Key, object>
    {
        protected override object Collect()
        {
            DebugLogger.Log("Key", $"Collected a new key!");
            return null;
        }

    }
}