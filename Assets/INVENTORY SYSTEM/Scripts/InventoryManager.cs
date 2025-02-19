using System.Collections.Generic;
using UnityEngine;

namespace KayosStudios.InventorySystem
{
    [RequireComponent(typeof(SphereCollider))]
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        [SerializeField] private InventoryData inventoryData;

        [Header("Item Detection Settings")]
        [SerializeField] SphereCollider sphereCollider;
        [SerializeField] float collisionRadius;
        [SerializeField] LayerMask collectibleLayer;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitiateSphere();
            }
            else
                Destroy(gameObject);
        }

        public void AddItem(ItemData item, int amount)
        {
            inventoryData.ModifyItemCount(item, amount);
        }

        public bool HasItem(ItemData item, int amount)
        {
            return inventoryData.CheckItemAvailability(item, amount);
        }

        private void InitiateSphere()
        {
            if(sphereCollider == null)
            {
                sphereCollider = GetComponent<SphereCollider>();
            }
            sphereCollider.isTrigger = true;
            sphereCollider.radius = collisionRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ItemComponent item))
            {
                DebugLogger.Log("InventorySystem", "Item detected within range. Beginning item collection.");
                item.Collect(); //Call the Collect() method
            }
        }

    }
}
