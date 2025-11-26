using UnityEngine;

public class Llave2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerKey2 = collision.GetComponent<Player>();

            if (playerKey2 != null)
            {
                playerKey2.tieneLlave2 = true;
                Destroy(gameObject);
            }
        }
    }
}
