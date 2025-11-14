using UnityEngine;

public class WaterAttackProjectile : MonoBehaviour
{
    public float speed = 4f;      
    public int damage = 1;
    public float lifeTime = 5f;

    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // Destruir después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player watersito = other.GetComponent<Player>();
            if (watersito != null)
                watersito.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
