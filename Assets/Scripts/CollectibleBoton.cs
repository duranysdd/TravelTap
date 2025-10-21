using UnityEngine;

public class CollectibleVida : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Detectado trigger con: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Item recogido! Intentando añadir al jugador.");
            Player player = other.GetComponent<Player>();
            
            if (player != null)
            {
                player.AddCollectible(value); 
                

            }
            else
            {
                Debug.LogError("El objeto con tag 'Player' no tiene el script 'Player' adjunto.");
            }

            Destroy(gameObject); 
        }
    }
}