using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    private float moveSpeed;
    private int hp;
    private Transform playerTransform;
    private IObjectPool<Enemy> pool;
    private bool released;

    public void Initialize(EnemyData data, Transform player, IObjectPool<Enemy> ownerPool)
    {
        moveSpeed = data.moveSpeed;
        hp = data.maxHP;
        playerTransform = player;
        pool = ownerPool;
        released = false;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (released) return;

        hp -= damage;
        if (hp <= 0)
        {
            released = true;
            pool.Release(this);
        }
    }
}