using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Movimiento de apertura")]
    public Transform puntoApertura;   //Un GameObject vacío donde debe llegar la puerta
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
            Player playerKey = collision.collider.GetComponent<Player>();

            if (playerKey != null && playerKey.tieneLlave)
            {
                abrir = true;
            }
        }
    }
}
