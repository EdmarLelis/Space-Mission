using System.Collections;
using UnityEngine;

public class AsteroidSpawner : Spawner
{
    [SerializeField] private Transform asteroidPrefab;

    private void Awake()
    {
        prefab = asteroidPrefab;
        minSpawnCount = 1;
        maxSpawnCount = 4;
        horizontalRange = 1f;
        verticalRange = 6f;
        minDelay = 1.2f;
        maxDelay = 1.7f;
    }
    
}
