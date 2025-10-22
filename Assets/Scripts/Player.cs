using System.Collections; 
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool isInvincible = false;

    [Header("Vectores")]
    public Vector2 playerVelocity;
    public Vector2 jumpVelocity;    
    public Vector2 input=Vector2.zero;

    [Header("Movimiento")]
    public float speed = 5;
    private Rigidbody2D rb2d;
    private float moveInput;
    public float Jump = 4;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.1f;
    public LayerMask graundLayer;
    private Animator anim;

    [Header("Inventario")]
    public int collectibles = 0;

    [Header("Vida del jugador")]
    public int maxHealth = 5;       
    public int currentHealth;        
    public Image[] hearts;           
    public Sprite fullHeart;         
    public Sprite emptyHeart;        

    [Header("Respawn")]
    public Transform respawnPoint;   
    public Transform nearRespawnPoint; 

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHeartsUI();

        if (respawnPoint == null)
            respawnPoint = transform;
    }

    [System.Obsolete]
    public void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb2d.linearVelocity = new Vector2(moveInput * speed, rb2d.velocity.y);

        if (moveInput != 0)
        {
            float baseScaleX = Mathf.Abs(transform.localScale.x);
            float currentScaleY = transform.localScale.y;
            float currentScaleZ = transform.localScale.z;
            float newScaleX = baseScaleX * Mathf.Sign(moveInput);
            transform.localScale = new Vector3(newScaleX, currentScaleY, currentScaleZ);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, Jump);
        }

        anim.SetFloat("walk", Mathf.Abs(moveInput));
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, graundLayer);
    }

    public void AddCollectible(int amount)
    {
        collectibles += amount;
    }

    // --- SISTEMA DE VIDA ---
    public void TakeDamage(int amount, bool causeRespawn = false)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHeartsUI();

        Debug.Log("Vida actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (causeRespawn)
        {
            // Daño por pinchos: reaparece en punto cercano
            if (nearRespawnPoint != null)
                transform.position = nearRespawnPoint.position;
            else
                transform.position = respawnPoint.position;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHeartsUI();
    }

    private void Die()
    {
        Debug.Log("Jugador ha muerto. Reiniciando...");
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            hearts[i].enabled = i < maxHealth;
        }
    }

    //INVULNERABILIDAD
    public IEnumerator Invincibility(float duration)
    {
        isInvincible = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color originalColor = sr.color;
            sr.color = new Color(1f, 1f, 1f, 0.5f); // semi-transparente
            yield return new WaitForSeconds(duration);
            sr.color = originalColor;
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }

        isInvincible = false;
    }
}
