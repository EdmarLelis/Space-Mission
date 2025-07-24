using UnityEngine;

public class SpaceCristalSpawner : Spawner
{
    [SerializeField] private Transform spaceCristalPrefab;

        private void Awake()
    {
        prefab = spaceCristalPrefab;
        minSpawnCount = 1;
        maxSpawnCount = 4;
        horizontalRange = 2f;
        verticalRange = 3f;
        minDelay = 3.5f;
        maxDelay = 7f;
    }
}
