using UnityEngine;

public class Puerta2 : MonoBehaviour
{
    [Header("Movimiento de apertura")]
    public Transform puntoApertura;
    public float velocidad = 2f;

    private bool abrir = false;

    private void Update()
    {
        if (abrir)
        {
            transform.position = Vector3.MoveTowards(transform.position, puntoApertura.position, velocidad * Time.deltaTime);
            if (Vector3.Distance(transform.position, puntoApertura.position) < 0.05f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player playerKey2 = collision.collider.GetComponent<Player>();

            if (playerKey2 != null && playerKey2.tieneLlave2)
            {
                abrir = true;
            }
        }
    }
}
