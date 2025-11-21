using UnityEngine;

public class WaterAttackProjectile : MonoBehaviour
{
    public float speed = 4f;
    public int damage = 1;

    private Transform target;
    private Vector2 direction;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // Dirección inicial hacia el jugador
        if (target != null)
            direction = (target.position - transform.position).normalized;

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (target == null) return;

        // Dirección hacia el jugador (suavizada)
        Vector2 newDirection = (target.position - transform.position).normalized;
        direction = Vector2.Lerp(direction, newDirection, 0.05f);

        // Mover el proyectil
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Rotación visual
        transform.Rotate(0, 0, 180 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
