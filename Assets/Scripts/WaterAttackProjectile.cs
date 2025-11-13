using UnityEngine;

public class WaterAttackProjectile : MonoBehaviour
{
   public float speed = 2f;            // Velocidad lenta
    public int damage = 1;              // Daño al jugador
    public float lifeTime = 5f;         // Desaparece después de unos segundos

    private Vector3 direction;

    void Start()
    {
        // El ataque se mueve hacia la izquierda (ajústalo según tu juego)
        direction = Vector3.left; 
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player watersito = other.GetComponent<Player>();

            if (watersito != null)
            {
                watersito.TakeDamage(damage);
            }

            Destroy(gameObject); // El ataque desaparece al golpear
        }
    }
}
