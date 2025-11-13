using UnityEngine;

public class WaterAttackProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 3f;

    void Start()
    {
        // Encuentra al jugador y calcula dirección
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().linearVelocity = dir * speed;

        }

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("💧 Jugador golpeado!");
            Destroy(gameObject);
        }
    }
}
