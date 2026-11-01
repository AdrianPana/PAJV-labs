using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private EnemyData[] enemyTypes;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float minSpawnDistance = 10f;
    [SerializeField] private float maxSpawnDistance = 14f;

    private ObjectPool<Enemy> enemyPool;
    private float spawnTimer;

    private void Awake()
    {
        enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(enemyPrefab),
            actionOnGet: enemy => enemy.gameObject.SetActive(true),
            actionOnRelease: enemy => enemy.gameObject.SetActive(false),
            actionOnDestroy: enemy => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: 20,
            maxSize: 100
        );
    }

    private void Update()
    {
        if (player == null || enemyTypes.Length == 0) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            Spawn();
            spawnTimer = spawnInterval;
        }
    }

    private void Spawn()
    {
        EnemyData data = enemyTypes[Random.Range(0, enemyTypes.Length)];

        Vector2 direction = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPos = player.position + (Vector3)(direction * distance);
        spawnPos.z = 0f;

        Enemy enemy = enemyPool.Get();
        enemy.transform.position = spawnPos;
        enemy.Initialize(data, player, enemyPool);
    }
}