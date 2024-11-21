using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public Vector2 spawnOffsetRange = new Vector2(0.5f, 0.5f);
    public int trashCount = 5;

    public void GenerateCluster(Vector3 position)
    {
        for (int i = 0; i < trashCount; i++)
        {
            GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];

            Vector3 offset = new Vector3(
                Random.Range(-spawnOffsetRange.x, spawnOffsetRange.x),
                Random.Range(-spawnOffsetRange.y, spawnOffsetRange.y),
                0
            );

            Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 180f));

            GameObject spawnedObject = Instantiate(prefab, position + offset, randomRotation, transform);
        }
    }
}
