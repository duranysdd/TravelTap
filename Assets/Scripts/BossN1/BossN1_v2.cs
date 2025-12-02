using UnityEngine;
using System.Collections;

public class BossN1_v2 : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 3f;
    public float jumpForce = 7f;

    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Detección del Player")]
    public Transform player;
    public float stopDistance = 1.2f;

    [Header("Vulnerabilidad")]
    public float vulnerableDuration = 2f;   // dura vulnerable
    public float blinkSpeed = 0.15f;        // velocidad del parpadeo
    public float vulnerableEvery = 5f;      // cada cuántos segundos se activa

    [Header("Vida")]
    public int maxHP = 3;
    private int currentHP;

    [Header("Daño al Player")]
    public int damage = 1;
    public float knockbackForce = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isGrounded;
    private bool isVulnerable = false;
    private bool isBlinking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentHP = maxHP;

        // Iniciar ciclo automático de vulnerabilidad
        StartCoroutine(VulnerabilityCycle());
    }

    void Update()
    {
        CheckGround();

        if (!isVulnerable)
        {
            MoveTowardPlayer();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // se congela
        }
    }

    void MoveTowardPlayer()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > stopDistance)
        {
            // Moverse hacia el player
            float dir = Mathf.Sign(player.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);

            // Saltar si hay diferencia de altura
            if (player.position.y - transform.position.y > 0.7f && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }

            // Voltear sprite
            sr.flipX = dir < 0;
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    IEnumerator VulnerabilityCycle()
    {
        while (true)
        {
            // Esperar el tiempo normal
            yield return new WaitForSeconds(vulnerableEvery);

            // Activar vulnerabilidad
            StartCoroutine(MakeVulnerable());
        }
    }

    IEnumerator MakeVulnerable()
    {
        isVulnerable = true;
        isBlinking = true;

        float timer = 0f;

        // parpadeo
        while (timer < vulnerableDuration)
        {
            sr.enabled = !sr.enabled;
            timer += blinkSpeed;
            yield return new WaitForSeconds(blinkSpeed);
        }

        // reset
        sr.enabled = true;
        isBlinking = false;
        isVulnerable = false;
    }

    public void TakeDamage(int amount)
    {
        if (!isVulnerable) return;

        currentHP -= amount;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player p = collision.collider.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(damage, x: transform.position.x);

                Rigidbody2D prb = collision.collider.GetComponent<Rigidbody2D>();
                if (prb != null)
                {
                    float dir = Mathf.Sign(collision.collider.transform.position.x - transform.position.x);
                    prb.AddForce(new Vector2(dir * knockbackForce, knockbackForce), ForceMode2D.Impulse);
                }
            }
        }
    }
}
