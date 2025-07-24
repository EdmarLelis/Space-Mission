using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    protected Transform prefab;
    protected  float delay;
    protected  int spawnCount;
    
    [Header("Spawner Atributes")]
    [SerializeField] protected  int minSpawnCount;
    [SerializeField] protected  int maxSpawnCount;
    [SerializeField] protected  float horizontalRange;
    [SerializeField] protected  float verticalRange;
    [SerializeField] protected  float minDelay;
    [SerializeField] protected  float maxDelay;

    protected virtual void Start()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab was not assigned in the inspector!");
            enabled = false;
            return;
        }
        StartCoroutine(Spawn());
    }

    protected virtual IEnumerator Spawn()
    {
        while (true)
        {
            spawnCount = Random.Range(minSpawnCount, maxSpawnCount);
            delay = Random.Range(minDelay, maxDelay);
            for (int i = 0; i < spawnCount; i++)
            {
                float horizontalPos = Random.Range(transform.position.x - horizontalRange, transform.position.x + horizontalRange);
                float verticalPos = Random.Range(transform.position.y - verticalRange, transform.position.y + verticalRange);

                Vector2 pos = new Vector2(horizontalPos, verticalPos);

                Instantiate(prefab, pos, transform.rotation);
            }

            yield return new WaitForSeconds(delay);
        }
    }
    
}
