using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public bool spawnEnemies = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && spawnEnemies)
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.transform.parent = transform; // Set the parent to the spawner
        spawnEnemies = false; // Stop spawning after all enemies are spawned
    }
}
