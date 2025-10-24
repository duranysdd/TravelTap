using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int damage = 1; 
    public float knockbackForce = 15f;
    public float invincibilityTime = 1f; 

    [Header("Puntos")]
    public Transform pointA; 
    public Transform pointB; 
    public float moveSpeed = 2f;
    public float tolerance = 0.3f;
    
    private Vector3 targetPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
        }

        if (pointA == null || pointB == null)
        {
            Debug.LogError("No hay puntos");
            enabled = false; 
            return;
        }

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x);
        transform.localScale = localScale;

        float distA = Vector3.Distance(transform.position, pointA.position);
        float distB = Vector3.Distance(transform.position, pointB.position);
        
        targetPosition = (distA < distB) ? pointB.position : pointA.position;
        
        Flip();
    }

    void FixedUpdate()
    {
        if (rb == null) return;
        
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
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }
        else
        {

            localScale.x = Mathf.Abs(localScale.x);
        }
        
        transform.localScale = localScale;
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