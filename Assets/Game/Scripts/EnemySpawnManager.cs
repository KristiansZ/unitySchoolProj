using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public Transform player;
    public float playerExclusionRadius = 10f;
    public float spawnRadius = 20f;
    public int maxEnemies = 20;
    public int initialEnemies = 8;
    public int enemiesToSpawn = 5;
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
        if (spawnedEnemies.Count < 6)
        {
            // Spawn additional enemies if the current count is less than the maximum allowed
            SpawnAdditionalEnemies(enemiesToSpawn);
        }
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