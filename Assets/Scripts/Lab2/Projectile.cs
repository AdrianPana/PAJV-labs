using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    private IObjectPool<Projectile> pool;
    private Vector3 direction;
    private float lifetime = 5f;
    private float timer = 0f;

    public void Launch(Vector3 position, Vector3 direction, IObjectPool<Projectile> ownerPool)
    {
        transform.position = position;
        this.direction = direction;
        pool = ownerPool;
        timer = 0f;
    }

    private void Update()
    {
        Debug.Log(transform.position);
        transform.position += direction * Time.deltaTime * 10f;
        Debug.Log(transform.position);

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            pool.Release(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
        pool.Release(this);
    }
}
