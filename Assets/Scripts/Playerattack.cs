using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 20;
    public Transform hitBox;       // Tu HitBox del jugador
    public float attackRange = 1f; // Radio del ataque
    public string bossTag = "Boss"; // Tag del boss

    // Llama a esta función cuando el jugador ataque (input o Animation Event)
    public void Attack()
    {
        if (hitBox == null) return;

        // Detecta todos los colliders en el rango del HitBox
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(hitBox.position, attackRange);

        foreach (Collider2D hit in hitObjects)
        {
            if (hit.CompareTag(bossTag))
            {
                BossHealth boss = hit.GetComponent<BossHealth>();
                if (boss != null)
                {
                    boss.TakeDamage(damage);
                    Debug.Log("Boss recibió daño: " + damage);
                }
            }
        }
    }

    // Visualización del rango de ataque en el editor
    private void OnDrawGizmosSelected()
    {
        if (hitBox == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitBox.position, attackRange);
    }
}
