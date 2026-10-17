
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    [Header("Spawn Settings")]
    public bool spawnOnlyOnce = false;

    private GameObject spawnedEnemy;
    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (spawnedEnemy != null)
            return;

        if (spawnOnlyOnce && hasSpawned)
            return;

        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Assign enemy prefab and spawn point!", this);
            return;
        }

        spawnedEnemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        hasSpawned = true;

        Debug.Log("Enemy spawned!");
    }
}
