using UnityEngine;

public class WaterAttackProjectile : MonoBehaviour
{
    public float speed = 4f;
    public int damage = 1;

    private Transform target;
    private Vector2 direction;

    void Start()
    {
        // Apuntar al Player porque ese es tu tag real
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            direction = (target.position - transform.position).normalized;
        }

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (target == null) return;

        Vector2 newDirection = (target.position - transform.position).normalized;
        direction = Vector2.Lerp(direction, newDirection, 0.05f);

        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        transform.Rotate(0, 0, 180 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // El proyectil debe dañar al Player
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
