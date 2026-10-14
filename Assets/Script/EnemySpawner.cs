
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
        // Detect Player 1 or Player 2
        if (!other.CompareTag("Player") )
            //&&
            //!other.CompareTag("Player")
        {
            return;
        }

        // Spawn only if there is no living spawned enemy
        if (spawnedEnemy != null)
            return;

        if (spawnOnlyOnce && hasSpawned)
            return;

        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogError(
                "EnemySpawner: Assign the enemy prefab and spawn point!",
                this
            );
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
