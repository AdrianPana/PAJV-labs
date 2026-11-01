using UnityEngine;
using UnityEngine.Pool;
namespace Lab2
{
    public class ProjectileWeapon : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float fireRate = 10f;
        private float fireCooldown = 0f;

        private ObjectPool<Projectile> projectilePool;

        private void Awake()
        {
            projectilePool = new ObjectPool<Projectile>(
                createFunc: () => Instantiate(projectilePrefab),
                actionOnGet: (projectile) => projectile.gameObject.SetActive(true),
                actionOnRelease: (projectile) => projectile.gameObject.SetActive(false),
                actionOnDestroy: (projectile) => Destroy(projectile.gameObject),
                collectionCheck: true,
                defaultCapacity: 10,
                maxSize: 20
            );
        }

        public void Fire(Vector3 direction)
        {
            Projectile projectile = projectilePool.Get();
            Debug.Log("Firing projectile from position: " + transform.position + " in direction: " + direction);
            projectile.Launch(transform.position, direction, projectilePool);
        }

        private void Update()
        {
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0f)
            {
                var closestEnemy = FindClosestEnemy();
                if (closestEnemy.HasValue && closestEnemy.Value.magnitude <= 5.0f)
                {
                    Vector3 directionToEnemy = (closestEnemy.Value - transform.position).normalized;
                    Fire(directionToEnemy);
                    fireCooldown = fireRate;
                }
            }
        }

        private Vector3? FindClosestEnemy()
        {
            Debug.Log("Finding closest enemy...");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
            return closestEnemy != null ? closestEnemy.transform.position : null;
        }
    }
}