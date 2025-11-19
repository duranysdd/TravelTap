using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public float speed = 1.5f;

    public int damage = 20;
    public float attackCooldown = 1f;
    private float cooldownTimer = 0f;

    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de detección → perseguir
        if (distance < detectionRange && distance > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // Si el jugador está dentro del rango de ataque → atacar
        if (distance <= attackRange && cooldownTimer <= 0)
        {
            Attack();
            cooldownTimer = attackCooldown;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void Attack()
    {
        anim.SetTrigger("attack");

        // Golpe real después de un pequeño delay
        Invoke("DealDamage", 0.25f);
    }

    void DealDamage()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
