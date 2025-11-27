using UnityEngine;

public class ColectibleVida : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.Curar(1);

            Destroy(gameObject);
        }
    }
}
