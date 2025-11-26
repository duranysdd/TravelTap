using UnityEngine;

public class ArcoDesbloquear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WatersitoAttack ws = other.GetComponent<WatersitoAttack>();
            if (ws != null)
            {
                ws.habilidadDesbloqueada = true;
                Debug.Log("Habilidad Arco Sagrado Desbloqueada!");
            }

            Destroy(gameObject); // opcional
        }
    }
}
