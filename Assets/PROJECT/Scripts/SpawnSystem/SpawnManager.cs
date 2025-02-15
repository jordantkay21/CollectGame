using System;
using System.Collections.Generic;
using UnityEngine;

namespace KayosStudios.TBD.SpawnSystem
{
    public enum SpawnTypes
    {

    }
    public interface ISpawnable
    {
        void OnSpawn();
    }

    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;

        public List<GameObject> spawnablePrefabs = new List<GameObject>();

        private Dictionary<SpawnableType, GameObject> spawnableObjects = new Dictionary<SpawnableType, GameObject>();

        public static event Action<GameObject> OnObjectSpawned;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            //Initialize dictionary from List
            foreach(var prefab in spawnablePrefabs)
            {
                if (prefab != null && Enum.TryParse(prefab.name.Replace(" ", "").Replace("_", "").Replace(".", ""), out SpawnableType type))
                {
                    spawnableObjects[type] = prefab;
                }
            }
        }

        public List<GameObject> GetSpawnablePrefabs()
        {
            return spawnablePrefabs;
        }

        public bool TryGetSpawnable(SpawnableType type, out GameObject prefab)
        {
            return spawnableObjects.TryGetValue(type, out prefab);
        }

        public GameObject Spawn(SpawnableType type, Vector3 position, Quaternion rotation)
        {
            if (!TryGetSpawnable(type, out GameObject prefab))
            {
                DebugLogger.Log("SpawnManager", $"Invalid spawn request: {type} is not a registered spawnable object!");
                return null;
            }

            GameObject spawnedObj = Instantiate(prefab, position, rotation);

            if (spawnedObj.TryGetComponent(out ISpawnable spawnable))
            {
                spawnable.OnSpawn();
            }

            OnObjectSpawned?.Invoke(spawnedObj);

            return spawnedObj;
        }

    }
}