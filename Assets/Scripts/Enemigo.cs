using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int damage = 1; 
    public float knockbackForce = 15f;
    public float invincibilityTime = 1f; 

    public float turnInterval = 2f; 
    public float checkRadius = 0.1f;
    public LayerMask graundLayer;
    public Transform groundCheck;
    
    private Rigidbody2D rb;
    private int direction = 1;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX; 
        }

        if (groundCheck == null)
        {
            Debug.LogError("¡Configura el Transform 'groundCheck' en el Inspector del enemigo!");
        }

        StartCoroutine(PatrolInPlace());
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, graundLayer);
        
    }

    private IEnumerator PatrolInPlace()
    {
        while (true)
        {
            yield return new WaitForSeconds(turnInterval);
            

            direction *= -1;
            

            transform.localScale = new Vector3(direction * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();

        if (player != null && !player.isInvincible) 
        {
            player.TakeDamage(damage); 
            player.Knockback(transform.position, knockbackForce);
            player.StartInvincibility(invincibilityTime);
        }
    }
}