using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab to spawn
    public int objectCount = 100; 
    public Vector3 spawnArea = new Vector3(40, 5, 40); // Area to spawn objects

    void Start()
    {
        SpawnRandomObject();
    }

    void SpawnRandomObject()
    {
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x ),
                Random.Range(0, spawnArea.y), 
                Random.Range(-spawnArea.z, spawnArea.z)
            );

   
            GameObject RandomItem = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

            // To ensure the spawned object doesn't have the SpawnObjects script
            if (RandomItem.GetComponent<SpawnObjects>())
            {
                Destroy(RandomItem.GetComponent<SpawnObjects>());
            }
        }
    }
}

