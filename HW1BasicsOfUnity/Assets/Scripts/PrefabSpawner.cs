using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to be spawned
    public float spawnDistance = 5f; // Distance from the spawner to spawn the prefab

    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Calculate spawn position in front of the spawner
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

            // Instantiate the prefab at the calculated position
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
