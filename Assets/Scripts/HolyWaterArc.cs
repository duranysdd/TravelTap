using UnityEngine;

public class HolyWaterArc : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damagePerTick = 1;
    public float tickInterval = 0.3f;

    private Rigidbody2D rb;
    private float tickTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(rb != null)
        {
            // Mueve el proyectil en la dirección que está mirando el objeto
            rb.linearVelocity = transform.right * speed;
        }

        Destroy(gameObject, lifetime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Boss"))
        {
            tickTimer += Time.deltaTime;
            if(tickTimer >= tickInterval)
            {
                tickTimer = 0f;
                BossWaterController boss = other.GetComponent<BossWaterController>();
                if(boss != null)
                    boss.TakeDamage(damagePerTick);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Boss"))
            tickTimer = 0f;
    }

    // Método para establecer la dirección al instanciar
    public void SetDirection(Vector2 direction)
    {
        direction.Normalize();              // Normaliza para que la velocidad sea correcta
        transform.right = direction;        // Gira el proyectil hacia la dirección
        if(rb != null)
            rb.linearVelocity = direction * speed; // Aplica la velocidad
    }
}
