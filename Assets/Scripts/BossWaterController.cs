using UnityEngine;

public class BossWaterController : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject waterAttackPrefab; 
    public Transform attackPoint;
    public float attackInterval = 3f;
    private bool isAttacking = false;

    [Header("Movimiento")]
    public Transform player;
    public float moveSpeed = 1.2f;
    public float stopDistance = 5f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Movimiento hacia el jugador
        if (distance > stopDistance)
        {
            Vector2 newPos = Vector2.Lerp(
                transform.position, 
                player.position, 
                moveSpeed * Time.deltaTime
            );

            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }

        // Mirar hacia el jugador
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void StartAttacking()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            InvokeRepeating(nameof(Attack), 1.5f, attackInterval);
        }
    }

    public void StopAttacking()
    {
        isAttacking = false;
        CancelInvoke(nameof(Attack));
    }

    private void Attack()
    {
        if (waterAttackPrefab == null || attackPoint == null)
        {
            Debug.LogWarning("⚠️ Asigna 'waterAttackPrefab' y 'attackPoint'");
            return;
        }

        // Instanciar la bola de agua
        Instantiate(waterAttackPrefab, attackPoint.position, Quaternion.identity);
    }
}
