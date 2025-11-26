using UnityEngine;

public class BossWaterController : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject waterAttackPrefab; 
    public Transform attackPoint;
    public float attackRange = 4f;
    public float attackInterval = 2f;
    private bool isAttacking = false;

    [Header("Movimiento")]
    public Transform player;
    public float moveSpeed = 1.5f;
    public float stopDistance = 5f;

    [Header("Teleport")]
    public Transform[] teleportPoints;
    public float teleportInterval = 8f;
    private float teleportTimer = 0f;
    public ParticleSystem teleportEffect;

    [Header("Vida")]
    public int maxHealth = 100;
    public int currentHealth;
    public UnityEngine.UI.Slider healthBar;
    private bool isDead = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.maxValue = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        HandleFlip();
        HandleMovement(distance);
        HandleAttack(distance);
        HandleTeleport();
    }

    // ➜ Girar hacia el jugador
    void HandleFlip()
    {
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // ➜ Movimiento e Idle
    void HandleMovement(float distance)
    {
        if (distance > stopDistance)
        {
            anim.SetBool("isWalking", true);

            Vector2 newPos = Vector2.Lerp(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    // ➜ Sistema de ataque automático
    void HandleAttack(float distance)
    {
        if (distance <= attackRange && !isAttacking)
        {
            StartAttacking();
        }
        else if (distance > attackRange && isAttacking)
        {
            StopAttacking();
        }
    }

    public void StartAttacking()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", true);

        InvokeRepeating(nameof(Attack), 0f, attackInterval);
    }

    public void StopAttacking()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", false);

        CancelInvoke(nameof(Attack));
    }

    private void Attack()
    {
        if (waterAttackPrefab == null || attackPoint == null) return;

        Instantiate(waterAttackPrefab, attackPoint.position, Quaternion.identity);
    }

    void HandleTeleport()
{
    if (teleportPoints.Length == 0) return;

    teleportTimer += Time.deltaTime;

    if (teleportTimer >= teleportInterval)
    {
        teleportTimer = 0f;
        Teleport();
    }
}

void Teleport()
{
    // Efecto de salida
    if (teleportEffect != null)
        Instantiate(teleportEffect, transform.position, Quaternion.identity);

    int randomIndex = Random.Range(0, teleportPoints.Length);

    transform.position = teleportPoints[randomIndex].position;

    // Efecto de entrada
    if (teleportEffect != null)
        Instantiate(teleportEffect, transform.position, Quaternion.identity);
}


    public void TakeDamage(int amount)
{
    if (isDead) return;

    currentHealth -= amount;

    if (healthBar != null)
        healthBar.value = currentHealth;

    anim.SetTrigger("Hurt");

    if (currentHealth <= 0)
        Die();
}

void Die()
{
    isDead = true;

    anim.SetBool("isWalking", false);
    anim.SetBool("isAttacking", false);

    anim.SetTrigger("Death");

    // Evitar movimiento
    CancelInvoke(nameof(Attack));

    // Desactivar colisionador
    Collider2D col = GetComponent<Collider2D>();
    if (col != null) col.enabled = false;

    Invoke(nameof(ShowVictoryScreen), 2f);
}

void ShowVictoryScreen()
{
    VictoryScreen.instance.ShowVictory();

}
}
