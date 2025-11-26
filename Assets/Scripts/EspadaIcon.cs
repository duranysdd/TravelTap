using UnityEngine;

public class ItemEspada : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();

            p.tieneEspada = true;
            GameManager.instance.tieneEspada = true;

            Destroy(gameObject);
        }
    }
}
