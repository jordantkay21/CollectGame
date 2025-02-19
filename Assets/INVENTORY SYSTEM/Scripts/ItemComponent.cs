using UnityEngine;

namespace KayosStudios.InventorySystem
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class ItemComponent : MonoBehaviour
    {
        [SerializeField] ItemData itemData; // Reference to SpcriptableObject

        private Collider collider;
        private Rigidbody rb;

        public ItemData ItemData => itemData; // Accessor for the item's data

        private void Start()
        {
            InitiateCollider();
            InitiateRigidbody();
        }

        public void Collect()
        {
            InventoryManager.Instance.AddItem(itemData, 1);
            DebugLogger.Log("InventorySystem", $"Collected: {itemData.itemName}");
            Destroy(gameObject); //Remove item from the world
        }

        private void InitiateCollider()
        {
            if (collider == null)
            {
                collider = GetComponent<Collider>();
            }
            
            collider.isTrigger = true;

            DebugLogger.Log("InventorySystem", $"{itemData.itemName}'s collider has been configured.", DebugLevel.Verbose);
        }

        private void InitiateRigidbody()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            rb.useGravity = false;
            rb.isKinematic = true;

            DebugLogger.Log("InventorySystem", $"{itemData.itemName}'s rigidbody has been configured.", DebugLevel.Verbose);
        }
    }
}