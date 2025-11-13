using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool isInvincible = false;
    [HideInInspector] private bool isKnockedBack = false;

    [Header("Movimiento")]
    public float speed = 5f;
    public float Jump = 8f;
    public float checkRadius = 0.1f;
    public Transform groundCheck;
    public LayerMask graundLayer;
    private bool atacando;
    private bool isGrounded;
    private float moveInput;
    private Rigidbody2D rb2d;
    private Animator anim;

    [Header("Respawn")]
    public Transform respawnPoint;
    public Transform nearRespawnPoint;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (respawnPoint == null)
            respawnPoint = transform;

        // Actualiza UI al iniciar
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateHearts();
            UIManager.instance.UpdateScore();
        }
    }

    private void Update()
    {
        if (isKnockedBack) return;

        if (!atacando) Movimientos();

        Animaciones();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, graundLayer);
    }

    public void Movimientos()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (atacando)
        {
            rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
            return;
        }

        rb2d.linearVelocity = new Vector2(moveInput * speed, rb2d.linearVelocity.y);

        if (moveInput != 0)
        {
            float newScaleX = Mathf.Abs(transform.localScale.x) * Mathf.Sign(moveInput);
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !atacando)
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, Jump);

        if (Input.GetKeyDown(KeyCode.Z) && !atacando && !isKnockedBack && isGrounded)
            Atacando();
    }

    public void Animaciones()
    {
        anim.SetFloat("walk", Mathf.Abs(moveInput));
        anim.SetBool("Atacando", atacando);
    }

    public void Knockback(Vector3 enemyPosition, float force)
    {
        isKnockedBack = true;

        Vector2 hitDirection = (transform.position - enemyPosition).normalized;
        Vector2 knockbackVector = new Vector2(hitDirection.x, 0.7f).normalized * force;

        rb2d.linearVelocity = Vector2.zero;
        rb2d.AddForce(knockbackVector, ForceMode2D.Impulse);

        StartCoroutine(StopKnockback(0.2f));
    }

    private IEnumerator StopKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        isKnockedBack = false;
    }

    public void StartInvincibility(float duration)
    {
        StartCoroutine(Invincibility(duration));
    }

    public IEnumerator Invincibility(float duration)
    {
        isInvincible = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color originalColor = sr.color;
            for (float i = 0; i < duration; i += 0.1f)
            {
                sr.enabled = !sr.enabled;
                yield return new WaitForSeconds(0.1f);
            }
            sr.enabled = true;
            sr.color = originalColor;
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }

        isInvincible = false;
    }

    // Contador de Items que ahora esta en el GameManager
    public void AddCollectible(int amount = 1)
    {
        GameManager.instance.AgregarColeccionable(amount);
    }

    // Contador-Vidas que ahora esta en  el GameManager
    public void TakeDamage(int amount, bool causeRespawn = false)
    {
        if (isInvincible) return;

        bool dead = GameManager.instance.TomarDaño(amount);

        if (dead)
        {
            Die();
        }
        else if (causeRespawn)
        {
            if (nearRespawnPoint != null)
                transform.position = nearRespawnPoint.position;
            else
                transform.position = respawnPoint.position;
        }
    }

    public void Heal(int amount)
    {
        GameManager.instance.Curar(amount);
    }

    private void Die()
    {
        Debug.Log("Te moriste wey jaja");
        transform.position = respawnPoint.position;

        // Si todo va bien se restaura la vida si se muere
        GameManager.instance.vidasActuales = GameManager.instance.maxVidas;
        if (UIManager.instance != null)
            UIManager.instance.UpdateHearts();
    }

    public void Atacando()
    {
        atacando = true;
        rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
    }

    public void NoAtacando()
    {
        atacando = false;
    }
}
