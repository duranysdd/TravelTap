using UnityEngine;

public class VenomProjectile : MonoBehaviour
{
    public float speed = 4f;
    private float fractionalDamage = 0.5f; // MEDIA VIDA

    private Vector3 direction;

    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
            direction = (player.position - transform.position).normalized;
        else
            direction = transform.right;

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        transform.Rotate(0, 0, 200 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FractionalDamageSystem.AddDamage(fractionalDamage);

            Destroy(gameObject);
        }

        if (collision.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
