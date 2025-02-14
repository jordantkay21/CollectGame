using KayosStudios.TBD.Inventory.Collectible;
using UnityEngine;

namespace KayosStudios.TBD.Spawnable
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] Transform spawnPosition;
        [SerializeField] GameObject spawnObj;

        public void SpawnKey()
        {
            if (spawnObj != null)
            {
                DebugLogger.Log("PlatformSpawner", $"Object already spawned");
                return;
            }

            DebugLogger.Log("PlatformSpawner", $"Attempting to spawn Key");

            spawnObj = SpawnManager.Instance.Spawn<KeyCard>(spawnPosition.position,Quaternion.identity);
            spawnObj.transform.parent = spawnPosition;
        }
    }
}
