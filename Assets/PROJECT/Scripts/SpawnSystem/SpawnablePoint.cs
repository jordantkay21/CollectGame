using UnityEngine;

namespace KayosStudios.TBD.SpawnSystem
{
    public abstract class SpawnablePoint : MonoBehaviour
    {
        [SerializeField] private SpawnableType spawnType; // Selectable in the Inspector
        [SerializeField] protected Transform spawnPosition;

        public SpawnableType SpawnType => spawnType; //Expose the spawn type

        public virtual void Spawn()
        {
            if (SpawnManager.Instance == null)
            {
                DebugLogger.Log("SpawnablePoint", $"SpawnManager instance is missing!");
                return;
            }

            GameObject spawnObject = SpawnManager.Instance.Spawn(spawnType, spawnPosition.position, spawnPosition.rotation);

            if (spawnObject == null)
            {
                DebugLogger.Log("SpawnablePoint",$"Failed to spawn {spawnType} at {gameObject.name}");
            }
        }
        
    }
}
