using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public Transform player;
    public float playerExclusionRadius = 20f;
    public float spawnRadius = 30f;
    public int maxEnemies = 100;
    public int initialEnemies = 8;
    public int enemiesToSpawn = 6;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private NavMeshSurface navMeshSurface;

    void Start()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
        SpawnInitialEnemies();
    }

    void SpawnInitialEnemies()
    {
        for (int i = 0; i < initialEnemies; i++)
        {
            Vector3 spawnPoint = GetRandomPointOnNavMesh();
            if (Vector3.Distance(spawnPoint, player.position) > playerExclusionRadius)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                spawnedEnemies.Add(enemy);
            }
        }
    }

    void Update()
    {
         int enemiesWithinRadius = CountEnemiesWithinRadius(player.position, 32f);

        if (enemiesWithinRadius < 2)
        {
            SpawnAdditionalEnemies(enemiesToSpawn);
        }
        if (spawnedEnemies.Count < 8)
        {
            SpawnAdditionalEnemies(enemiesToSpawn);
        }
    }
    int CountEnemiesWithinRadius(Vector3 center, float radius)
    {
        int count = 0;
        
        foreach (GameObject enemy in spawnedEnemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, center);
            if (distance <= radius)
            {
                count++;
            }
        }

        return count;
    }

    void SpawnAdditionalEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPoint = GetRandomPointOnNavMesh();
            if (Vector3.Distance(spawnPoint, player.position) > playerExclusionRadius)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                spawnedEnemies.Add(enemy);
            }
        }
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomPoint = player.position + Random.insideUnitSphere * spawnRadius;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, NavMesh.AllAreas);
        return hit.position;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }
}