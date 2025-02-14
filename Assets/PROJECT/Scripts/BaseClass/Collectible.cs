using KayosStudios.TBD.Spawnable;
using UnityEngine;

namespace KayosStudios.TBD.Inventory.Collectible
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Collectible<T,TData> : MonoBehaviour, ISpawnable where T : Collectible<T, TData>
    {
        [SerializeField] float collisionRadius;
        protected SphereCollider sphereCollider;

        public abstract void OnSpawn();


        protected virtual void Awake()
        {
            InitSphereCollider();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                DebugLogger.Log("Collectible", $"Player has been detected. Collecting {typeof(T).Name} now.");
                BaseCollect();
            }
        }


        private void BaseCollect()
        {
            TData data = Collect();
            DebugLogger.Log("Collectible", $"Data has been sourced from {typeof(T).Name}, invoking event now.");
            Destroy(gameObject);
        }

        protected abstract TData Collect();

        private void InitSphereCollider()
        {
            if (sphereCollider == null)
            {
                sphereCollider = GetComponent<SphereCollider>();
            }

            sphereCollider.radius = collisionRadius;
            sphereCollider.isTrigger = true;
        }


    }
}