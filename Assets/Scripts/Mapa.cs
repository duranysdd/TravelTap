using UnityEngine;

public class MapItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player pm = collision.GetComponent<Player>();

            if (pm != null)
            {
                pm.tieneMapa = true;
                Destroy(gameObject);
            }
        }
    }
}
