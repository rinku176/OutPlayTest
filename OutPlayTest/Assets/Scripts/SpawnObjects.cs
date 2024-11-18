using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab to spawn
    public int objectCount = 10; // Total number of objects to spawn
    public Vector2 spawnAreaSize = new Vector2(50, 50); // Area in which to spawn objects

    void Start()
    {
        // Run this code only on the spawner GameObject
        SpawnRandomObject();
    }

    void SpawnRandomObject()
    {
        for (int i = 0; i < objectCount; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            // Ensure only one spawner script runs, and the spawned objects do not inherit the spawner script
            GameObject spawnedObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);

            // Ensure the spawned object doesn't have the RandomObjectSpawner2D script
            if (spawnedObject.GetComponent<SpawnObjects>())
            {
                Destroy(spawnedObject.GetComponent<SpawnObjects>());
            }
        }
    }
}

