using System;
using UnityEngine;

namespace KayosStudios.TBD.Spawnable
{
    public interface ISpawnable
    {
        void OnSpawn();
    }

    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;

        [SerializeField] GameObject[] spawnablePrefabs; //Assign in Inspector
        [SerializeField] Transform[] spawnPoints;

        public event Action<GameObject> OnObjectSpawned;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public GameObject Spawn<T>(Vector3 position, Quaternion rotation) where T: MonoBehaviour, ISpawnable
        {
            GameObject prefab = GetPrefabOfType<T>();

            if(prefab == null)
            {
                DebugLogger.Log("SpawnManager", $"No Prefab of Type {typeof(T).Name} found!");
                return null;
            }

            GameObject spawnedObject = Instantiate(prefab, position, rotation);
            if (spawnedObject.TryGetComponent(out ISpawnable spawnable))
            {
                spawnable.OnSpawn();
            }

            OnObjectSpawned?.Invoke(spawnedObject);

            return spawnedObject;
        }

        private GameObject GetPrefabOfType<T>() where T : MonoBehaviour, ISpawnable
        {
            foreach (GameObject prefab in spawnablePrefabs)
            {
                if (prefab.GetComponent<T>() != null) 
                    return prefab;
            }
                return null;
        }

        public void SpawnRandom()
        {
            if (spawnablePrefabs.Length == 0 || spawnPoints.Length == 0) return;

            GameObject randomPrefab = spawnablePrefabs[UnityEngine.Random.Range(0, spawnablePrefabs.Length)];
            Transform randomSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

            GameObject spawnedObject = Instantiate(randomPrefab, randomSpawnPoint.position, Quaternion.identity);
            OnObjectSpawned?.Invoke(spawnedObject);
        }
    }
}