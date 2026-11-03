using UnityEngine;
using UnityEngine.Pool;

namespace Lab2
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyData[] enemyTypes;
        [SerializeField] private Transform player;
        [SerializeField] private EnemyFactory enemyFactory;

        private ObjectPool<Enemy> enemyPool;
        private float spawnTimer;

        private void Awake()
        {
            enemyPool = new ObjectPool<Enemy>(
                createFunc: () => Spawn(),
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
                spawnTimer = LevelManager.Instance.currentEnemySpawnInterval;
            }
        }

        private Enemy Spawn()
        {
            EnemyData data = enemyTypes[Random.Range(0, LevelManager.Instance.enemyTypes)];
            return enemyFactory.Create(data, player, enemyPool);
        }
    }
}