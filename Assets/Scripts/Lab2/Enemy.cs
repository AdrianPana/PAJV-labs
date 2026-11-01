using UnityEngine;
using UnityEngine.Pool;

namespace Lab2
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject XpPrefab;

        private float moveSpeed;
        private int hp;
        private Transform playerTransform;
        private IObjectPool<Enemy> pool;
        private bool released;

        private float attackCooldown;
        private float attackTimer = 0f;

        public void Initialize(EnemyData data, Transform player, IObjectPool<Enemy> ownerPool)
        {
            moveSpeed = data.moveSpeed;
            hp = data.maxHP;
            playerTransform = player;
            pool = ownerPool;
            released = false;
            attackCooldown = data.attackCooldown;
            attackTimer = 0f;
        }

        private void Update()
        {
            if (playerTransform == null) return;

            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            attackTimer -= Time.deltaTime;
        }

        public void TakeDamage(int damage)
        {
            if (released) return;

            hp -= damage;
            if (hp <= 0)
            {
                released = true;
                pool.Release(this);
                if (XpPrefab != null)
                {
                    Instantiate(XpPrefab, transform.position, Quaternion.identity);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TryAttack(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            TryAttack(collision);
        }

        private void TryAttack(Collider2D collision)
        {
            if (collision.CompareTag("Player") && attackTimer <= 0)
            {
                collision.GetComponent<Player>().TakeDamage(1);
                attackTimer = attackCooldown;
            }
        }
    }
}