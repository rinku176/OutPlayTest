using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab to spawn
    public int objectCount = 10; 
    public Vector3 spawnArea = new Vector3(20, 0, 20); // Area to spawn objects

    void Start()
    {
        // Run this code only on the spawner GameObject
        SpawnRandomObject();
    }

    void SpawnRandomObject()
    {
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(0, spawnArea.y), 
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );

   
            GameObject RandomItem = Instantiate(objectPrefab, randomPosition, Quaternion.identity);

            // To ensure the spawned object doesn't have the SpawnObjects script
            if (RandomItem.GetComponent<SpawnObjects>())
            {
                Destroy(RandomItem.GetComponent<SpawnObjects>());
            }
        }
    }
}

