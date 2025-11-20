using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 20;
    public Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!player) return;

        // solo daña si el jugador realmente está atacando
        if (!player.GetIsAttacking()) return;

        // si golpea a un boss
        BossHealth boss = other.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        
    }
    
}
