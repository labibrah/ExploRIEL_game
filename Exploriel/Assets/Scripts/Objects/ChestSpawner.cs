using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject ChestPrefab;
    public bool spawnChest = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && spawnChest)
        {
            SpawnChest();
        }
    }

    void SpawnChest()
    {
        GameObject chest = Instantiate(ChestPrefab, transform.position, Quaternion.identity);
        chest.transform.parent = transform; // Set the parent to the spawner
        spawnChest = false; // Stop spawning after the chest is spawned
    }
}

