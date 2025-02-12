using KayosStudios.TBD.BaseClass;
using UnityEngine;

namespace KayosStudios.TBD.Inventory.Items
{
    public class Coin : Collectible<Coin, int>
    {
        [SerializeField] int amount = 10;

        protected override int Collect()
        {
            DebugLogger.Log("Coin", $"Collected {amount} coins!");
            return amount;
        }
    }
}