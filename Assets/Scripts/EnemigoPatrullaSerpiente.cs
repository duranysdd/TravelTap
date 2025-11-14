using UnityEngine;

public class EnemigoPatrullaSerpiente : MonoBehaviour
{
     public float velocidad = 2f;
    public Transform puntoIzquierdo;
    public Transform puntoDerecho;
    private bool moviendoDerecha = true;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (moviendoDerecha)
        {
            rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);
            sr.flipX = false;
            if (transform.position.x >= puntoDerecho.position.x)
                moviendoDerecha = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y);
            sr.flipX = true;
            if (transform.position.x <= puntoIzquierdo.position.x)
                moviendoDerecha = true;
        }
    }

    // Si choca con el jugador, puede hacer daño
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Jugador golpeado!");
            // Aquí podrías llamar a player.TakeDamage(1);
        }
    }
}
