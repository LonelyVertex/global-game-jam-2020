using UnityEngine;

public class BoxSpawn : GameManagerStateListener
{
    [Header("Spawn")]
    [SerializeField] private GameObject boxPrefab = default;
    [SerializeField] private float firstSpawn = default;
    [SerializeField] private float spawnInterval = default;
    

    void Spawn()
    {
        Instantiate(boxPrefab, SpawnManager.Instance.RandomSpawnPosition(), Quaternion.identity);
    }

    protected override void OnGameManagerStateChange(GameManager.State newState)
    {
        if (newState == GameManager.State.Game)
        {
            InvokeRepeating(nameof(Spawn), firstSpawn, spawnInterval);
        }
        else
        {
            CancelInvoke(nameof(Spawn));
        }
    }
}

