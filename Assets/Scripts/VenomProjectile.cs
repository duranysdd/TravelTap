using UnityEngine;
using System.Collections;

public class VenomProjectile : MonoBehaviour
{
    public float speed = 4f;
    private float fractionalDamage = 0.5f; // MEDIA VIDA

    private bool canHit = false;
    private Vector3 direction;

    void Start()
    {
        // 🔥 Activa daño después de 0.1 segundos
        StartCoroutine(EnableDamage());

        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
            direction = (player.position - transform.position).normalized;
        else
            direction = transform.right;

        Destroy(gameObject, 5f);
    }

    IEnumerator EnableDamage()
    {
        yield return new WaitForSeconds(0.1f);
        canHit = true;    // ✔ Ya puede golpear al jugador
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        transform.Rotate(0, 0, 200 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canHit) return;  // ❗ Evita que se golpee al disparador

        if (collision.CompareTag("Player"))
        {
            FractionalDamageSystem.AddDamage(fractionalDamage); // ✔ medio corazón
            Destroy(gameObject);
        }

        if (collision.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
