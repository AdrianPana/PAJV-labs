using Lab2;
using UnityEngine;
using UnityEngine.Pool;

namespace Lab2
{
    public class EnemyFactory : MonoBehaviour
    {
        private float minSpawnDistance = 7f;
        private float maxSpawnDistance = 10f;

        public Enemy Create(EnemyData data, Transform player, IObjectPool<Enemy> enemyPool)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 spawnPos = player.position + (Vector3)(direction * distance);
            spawnPos.z = 0f;

            Enemy enemy = Instantiate(data.prefab, spawnPos, Quaternion.identity);
            enemy.Initialize(data, player, enemyPool);
            return enemy;
        }
    }
}