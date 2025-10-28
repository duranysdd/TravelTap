using UnityEngine;

public class CaidaSuelo : MonoBehaviour
{
    [Header("Configuración")]
    public Transform puntoRespawn;
    public string tagJugador = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagJugador))
        {
            other.transform.position = puntoRespawn.position;

            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; 
            }
        }
    }
}
