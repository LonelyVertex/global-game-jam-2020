using UnityEngine;

public class BoxSpawn : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private float firstSpawn;
    [SerializeField] private float spawnInterval;
    
    void Start()
    {
        InvokeRepeating(nameof(Spawn), firstSpawn, spawnInterval);
    }

    void Spawn()
    {
        Instantiate(boxPrefab, SpawnManager.Instance.RandomSpawnPosition(), Quaternion.identity);
    }
}
