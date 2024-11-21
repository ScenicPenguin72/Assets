using UnityEngine;

public class RandomTrash : MonoBehaviour
{
    public GameObject trashPrefab;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 5f;
    public float minSecondTrashDelay = 4f;

    private bool spawnSecondTrash;

    void Start()
    {
        spawnSecondTrash = Random.value > 0.5f;
        StartCoroutine(SpawnTrash());
    }

    System.Collections.IEnumerator SpawnTrash()
    {
        while (true)
        {
            float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(randomDelay);

            SpawnTrashCluster(trashPrefab);

            if (spawnSecondTrash)
            {
                float secondTrashDelay = Random.Range(minSecondTrashDelay, maxSpawnDelay);
                yield return new WaitForSeconds(secondTrashDelay);

                SpawnTrashCluster(trashPrefab);
            }
        }
    }

    void SpawnTrashCluster(GameObject prefab)
    {
        if (prefab != null)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
