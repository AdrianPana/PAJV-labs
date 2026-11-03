using UnityEngine;
using UnityEngine.Pool;
namespace Lab2
{
    public class ProjectileWeapon : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float fireRateUpgradeMultiplier = 1f;
        [SerializeField] private float range = 1f;
        [SerializeField] private float rangeUpgradeMultiplier = 1f;
        private int damageUpgradeBonus = 0;
        private float fireCooldown = 0f;

        private ObjectPool<Projectile> projectilePool;

        private void Awake()
        {
            projectilePool = new ObjectPool<Projectile>(
                createFunc: () => Create(),
                actionOnGet: (projectile) => OnGet(projectile),
                actionOnRelease: (projectile) => projectile.gameObject.SetActive(false),
                actionOnDestroy: (projectile) => Destroy(projectile.gameObject),
                collectionCheck: true,
                defaultCapacity: 10,
                maxSize: 20
            );
        }

        private Projectile Create()
        {
            Projectile projectile = Instantiate(projectilePrefab);
            projectile.damage = 1 + damageUpgradeBonus;
            return projectile;
        }

        private void OnGet(Projectile projectile)
        {
            projectile.damage = 1 + damageUpgradeBonus;
        }

        public void Fire(Vector3 direction)
        {
            Projectile projectile = projectilePool.Get();
            projectile.Launch(transform.position, direction, projectilePool);
        }

        private void Update()
        {
            if (fireRate <= 0f) return; // Prevent firing if fireRate is zero or negative
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0f)
            {
                var closestEnemy = FindClosestEnemy();
                if (closestEnemy.HasValue && Vector3.Distance(transform.position, closestEnemy.Value) <= range)
                {
                    Vector3 directionToEnemy = (closestEnemy.Value - transform.position).normalized;
                    Fire(directionToEnemy);
                    fireCooldown = fireRate;
                }
            }
        }

        private Vector3? FindClosestEnemy()
        {
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

        public void UpgradeFireRate()
        {
            if (fireRate == 0f) 
                fireRate = 3f;
            else
                fireRate *= fireRateUpgradeMultiplier;
            GameStateMachine.Instance.ChangeState(new MainState());
        }

        public void UpgradeRange()
        {
            if (range == 0f)
                range = 1f;
            else
                range *= rangeUpgradeMultiplier;
            GameStateMachine.Instance.ChangeState(new MainState());
        }

        public void UpgradeDamage()
        {
            damageUpgradeBonus += 1;
            GameStateMachine.Instance.ChangeState(new MainState());
        }
    }
}