using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject PortalPrefab;
    public bool spawnPortal = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && spawnPortal)
        {
            SpawnPortal();
        }
    }

    void SpawnPortal()
    {
        GameObject enemy = Instantiate(PortalPrefab, transform.position, Quaternion.identity);
        enemy.transform.parent = transform; // Set the parent to the spawner
        spawnPortal = false; // Stop spawning after the portal is spawned
    }
}
