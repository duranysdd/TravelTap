using UnityEngine;

public class BossWaterController : MonoBehaviour
{
    public GameObject waterAttackPrefab; // prefab del ataque de agua sucia
    public Transform attackPoint;        // punto desde donde dispara
    public float attackInterval = 2f;    // cada cuántos segundos ataca
    private bool isAttacking = false;

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
            Debug.LogWarning("Falta asignar 'waterAttackPrefab' o 'attackPoint' en el boss.");
        }
    }
}

