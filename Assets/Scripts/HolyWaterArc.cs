using UnityEngine;

public class HolyWaterArc : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 1.2f;
    public int damage = 5;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Movimiento recto hacia adelante
        if (rb != null)
            rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifetime);
    }

  private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Boss"))
    {
        BossWaterController boss = other.GetComponent<BossWaterController>();
        
        if (boss != null)
            boss.TakeDamage(damage);

        Destroy(gameObject);
    }
}
}
