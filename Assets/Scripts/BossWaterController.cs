using UnityEngine;

public class BossWaterController : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject waterAttackPrefab; // Prefab del ataque de agua sucia
    public Transform attackPoint;        // Punto desde donde dispara
    public float attackInterval = 2f;    // Intervalo entre ataques
    private bool isAttacking = false;

    [Header("Movimiento")]
    public Transform player;             // Jugador (watersito)
    public float moveSpeed = 2f;         // Velocidad del boss
    public float stopDistance = 4f;      // Distancia mínima para dejar de moverse

    void Update()
    {
        if (player == null) return;

        // Movimiento hacia el jugador
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            // Mover hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }

        // Girar sprite según la dirección
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);  // mirando a la derecha
        else
            transform.localScale = new Vector3(-1, 1, 1); // mirando a la izquierda
    }

    // Llamado desde BossTrigger al entrar en la zona
    public void StartAttacking()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            InvokeRepeating(nameof(Attack), 1f, attackInterval);
        }
    }

    public void StopAttacking()
    {
        isAttacking = false;
        CancelInvoke(nameof(Attack));
    }

    private void Attack()
    {
        if (waterAttackPrefab != null && attackPoint != null)
        {
            Instantiate(waterAttackPrefab, attackPoint.position, attackPoint.rotation);
        }
        else
        {
            Debug.LogWarning("⚠️ Falta asignar 'waterAttackPrefab' o 'attackPoint' en el boss.");
        }
    }
}
