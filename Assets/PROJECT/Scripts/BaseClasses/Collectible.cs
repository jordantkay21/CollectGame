using System;
using UnityEngine;

namespace KayosStudios.TBD.BaseClass
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Collectible<T, TData> : MonoBehaviour where T : Collectible<T, TData>
    {
        public static event Action<TData> OnCollected;
        [SerializeField] float collisionRadius;
        protected SphereCollider sphereCollider;

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
            DebugLogger.Log("Collectible", $"Data has been collected from {typeof(T).Name}, invoking event now.");
            OnCollected?.Invoke(data);
            Destroy(gameObject);
        }
        protected abstract TData Collect();

        protected void InitSphereCollider()
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