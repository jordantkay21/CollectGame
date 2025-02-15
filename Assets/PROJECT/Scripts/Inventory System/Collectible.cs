using KayosStudios.TBD.SpawnSystem;
using System;
using UnityEngine;

namespace KayosStudios.TBD.CollectibleLogic
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class Collectible<T,TData> : MonoBehaviour, ISpawnable where T : Collectible<T,TData>
    {
        [SerializeField] float collisionRadius;

        protected SphereCollider sphereCollider;

        public static event Action<TData> OnCollection;
        
        
        public abstract void OnSpawn();
        protected abstract TData Collect();


        private void Awake()
        {
            InitSphereCollider();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                DebugLogger.Log("Colelctible", $"Player has been detected. Collecting {typeof(T).Name} now.");
                BaseCollect();
            }
        }

        private void BaseCollect()
        {
            TData data = Collect();
            DebugLogger.Log("Collectible", $"Data [{data}] has been sourced from {typeof(T).Name}");
            OnCollection?.Invoke(data);
            Destroy(gameObject);
        }

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