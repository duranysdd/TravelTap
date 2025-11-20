using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerKey = collision.GetComponent<Player>();

            if (playerKey != null)
            {
                playerKey.tieneLlave = true;
                Destroy(gameObject);
            }
        }
    }
}
