using UnityEngine;

public class ViboraController : MonoBehaviour
{
    [Header("Patrulla")]
    public Transform pointA;
    public Transform pointB;
    private Transform currentPoint;
    public float speed = 2f;
    public float tolerance = 0.05f;

    [Header("Ataque")]
    public GameObject venomProjectile;
    public Transform firePoint;
    public float attackCooldown = 2f;
    public float attackRange = 5f;
    private float attackTimer;

    [Header("Animación")]
    public Animator animator;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip attackSound;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentPoint = pointA;
        attackTimer = attackCooldown;

        sr.flipX = currentPoint.position.x < transform.position.x;
    }

    void FixedUpdate()
    {
        Patrol();
    }

    void Update()
    {
        AttackTimerUpdate();
    }

    // -------------------------
    //     PATRULLA
    // -------------------------
    void Patrol()
    {
        animator.SetBool("Walking", true);

        Vector2 direction = (currentPoint.position - transform.position).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, currentPoint.position) < tolerance)
        {
            currentPoint = (currentPoint == pointA) ? pointB : pointA;
            FlipToPoint();
        }
    }

    // -------------------------
    //     ATAQUE AUTOMÁTICO
    // -------------------------
    void AttackTimerUpdate()
    {
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
            return;
        }

    // Si el jugador está muy lejos, no atacar
         if (Vector2.Distance(transform.position, player.position) > attackRange)
            return;

        LookAtPlayer();

        animator.SetTrigger("Attack");

        if (audioSource && attackSound)
            audioSource.PlayOneShot(attackSound);

        Instantiate(venomProjectile, firePoint.position, firePoint.rotation);

        attackTimer = attackCooldown;
    }

    // -------------------------
    //     GIRO
    // -------------------------
    private void FlipToPoint()
    {
        if (sr == null) return;
        sr.flipX = currentPoint.position.x < transform.position.x;
    }

   private void LookAtPlayer()
{
    if (player == null) return;

    bool flip = player.position.x < transform.position.x;

    // Girar el sprite
    sr.flipX = flip;

    // 🔥 Girar el firePoint
    Vector3 scale = firePoint.localScale;
    scale.x = flip ? -1 : 1;
    firePoint.localScale = scale;
}

    // ------------------------------------------------------
    //       EMPUJAR A WATERSITO Y EVITAR QUE SE SUBA
    // ------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player p = collision.collider.GetComponent<Player>();

            if (p != null && !p.isInvincible)
            {
                // empujón
                p.Knockback(transform.position, 6f);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Evita que Watersito pueda quedarse encima como plataforma
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D plRb = collision.collider.GetComponent<Rigidbody2D>();
            if (plRb != null)
            {
                float dir = Mathf.Sign(collision.collider.transform.position.x - transform.position.x);
                plRb.linearVelocity = new Vector2(dir * 4f, plRb.linearVelocity.y);
            }
        }
    }
}
