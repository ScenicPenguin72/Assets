using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyConfig
{
    public GameObject enemyPrefab;
    public float spawnInterval;
    public float blockRadius;
    public float blockDuration;
    public bool isXActive;
    public Transform waypoint1;
    public Transform waypoint2;
    public float speed = 2f;
    public bool moveOnX = true;
    public int direction = 1;
}

public class Spawner : MonoBehaviour
{
    public List<EnemyConfig> enemyConfigs;

    private List<BlockedPosition> blockedPositions = new List<BlockedPosition>();
    private float[] timers;

    private class BlockedPosition
    {
        public Vector3 position;
        public float timer;

        public BlockedPosition(Vector3 pos, float time)
        {
            position = pos;
            timer = time;
        }
    }

    void Start()
    {
        timers = new float[enemyConfigs.Count];
    }

    void Update()
    {
        UpdateBlockedPositions();

        for (int i = 0; i < enemyConfigs.Count; i++)
        {
            timers[i] += Time.deltaTime;

            if (timers[i] >= enemyConfigs[i].spawnInterval)
            {
                SpawnEnemy(enemyConfigs[i]);
                timers[i] = 0;
            }
        }
    }

    void UpdateBlockedPositions()
    {
        for (int i = blockedPositions.Count - 1; i >= 0; i--)
        {
            blockedPositions[i].timer -= Time.deltaTime;
            if (blockedPositions[i].timer <= 0)
            {
                blockedPositions.RemoveAt(i);
            }
        }
    }

    void SpawnEnemy(EnemyConfig config)
    {
        if (config.waypoint1 == null || config.waypoint2 == null) return;

        Vector3 spawnPosition;
        int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            if (config.isXActive)
            {
                float randomX = Random.Range(config.waypoint1.position.x, config.waypoint2.position.x);
                spawnPosition = new Vector3(randomX, config.waypoint1.position.y, config.waypoint1.position.z);
            }
            else
            {
                float randomY = Random.Range(config.waypoint1.position.y, config.waypoint2.position.y);
                spawnPosition = new Vector3(config.waypoint1.position.x, randomY, config.waypoint1.position.z);
            }

            if (!IsPositionBlocked(spawnPosition, config.blockRadius))
            {
                GameObject enemy = Instantiate(config.enemyPrefab, spawnPosition, Quaternion.identity);
                ConfigureEnemy(enemy, config);
                blockedPositions.Add(new BlockedPosition(spawnPosition, config.blockDuration));
                return;
            }
        }
    }

    void ConfigureEnemy(GameObject enemy, EnemyConfig config)
    {
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.speed = config.speed;
            movement.moveOnX = config.moveOnX;
            movement.direction = config.direction;
        }
    }

    bool IsPositionBlocked(Vector3 position, float radius)
    {
        foreach (var blocked in blockedPositions)
        {
            if (Vector3.Distance(position, blocked.position) <= radius)
            {
                return true;
            }
        }
        return false;
    }
}
