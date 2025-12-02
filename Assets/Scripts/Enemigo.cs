using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Stats del Enemigo")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Daño al Jugador")]
    public int damage = 1;
    public float knockbackForce = 15f;
    public float invincibilityTime = 1f;

    [Header("Patrullaje")]
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public float tolerance = 0.3f;

    [Header("Invencibilidad del enemigo al recibir daño")]
    public float enemyInvTime = 0.3f;
    private bool isInvincible = false;

    private Vector3 targetPosition;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (pointA == null || pointB == null)
        {
            Debug.LogError("No asignaste pointA o pointB al enemigo.");
            enabled = false;
            return;
        }

        float distA = Vector3.Distance(transform.position, pointA.position);
        float distB = Vector3.Distance(transform.position, pointB.position);
        targetPosition = (distA < distB) ? pointB.position : pointA.position;

        Flip();
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;

        if (Mathf.Abs(targetPosition.x - currentPosition.x) < tolerance)
        {
            targetPosition = (targetPosition == pointB.position) ? pointA.position : pointB.position;
            Flip();
        }

        float directionX = Mathf.Sign(targetPosition.x - currentPosition.x);
        rb.linearVelocity = new Vector2(directionX * moveSpeed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        bool lookLeft = targetPosition.x < transform.position.x;

        Vector3 localScale = transform.localScale;

        if (lookLeft)
            localScale.x = -Mathf.Abs(localScale.x);
        else
            localScale.x = Mathf.Abs(localScale.x);

        transform.localScale = localScale;
    }

    // =====================
    //   RECIBIR DAÑO
    // =====================
    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(EnemyInvincibility());
    }

    private System.Collections.IEnumerator EnemyInvincibility()
    {
        isInvincible = true;

        for (int i = 0; i < 5; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.05f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }

        isInvincible = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    // =====================
    //   COLISIÓN
    // =====================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();

        if (player == null) return;

        // Si el Player está atacando → ENEMIGO RECIBE DAÑO
        if (player.GetIsAttacking())
        {
            TakeDamage(1);
            return;
        }

        // Si NO está atacando → EL ENEMIGO LE PEGA AL PLAYER
        if (!player.isInvincible)
        {
            player.TakeDamage(damage);
            player.Knockback(transform.position, knockbackForce);
            player.StartInvincibility(invincibilityTime);
        }
    }
}
