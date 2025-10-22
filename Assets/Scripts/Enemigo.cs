using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int damage = 1;                  
    public float knockbackForce = 5f;      
    public float invincibilityTime = 1f;    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();

        if (player != null && !player.isInvincible) 
        {

            Vector2 hitDirection = (player.transform.position - transform.position).normalized;
            Vector2 knockback = new Vector2(hitDirection.x, 0.5f) * knockbackForce;

            player.TakeDamage(damage, false);

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; 
                rb.AddForce(knockback, ForceMode2D.Impulse);
            }

            player.StartCoroutine(player.Invincibility(invincibilityTime));
        }
    }
}
