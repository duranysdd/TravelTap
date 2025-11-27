using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Vida")]
    public int vida = 10;

    [Header("Daño del golpe al suelo")]
    public int dañoGolpe = 1;
    public float radioGolpe = 1f;
    public LayerMask playerLayer;

    [Header("Movimiento")]
    public float velocidad = 2f;
    public Transform puntoA;
    public Transform puntoB;
    private Transform objetivoActual;

    [Header("Tiempos")]
    public float tiempoEntreAtaques = 2f;
    public float tiempoGrito = 1.2f;
    public float tiempoGolpe = 0.8f;

    private Animator anim;
    private bool puedeSerGolpeado = false;
    private bool atacando = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        objetivoActual = puntoA;

        StartCoroutine(CicloBoss());
    }

    private void Update()
    {
        if (!atacando)
            Patrullar();
    }

    void Patrullar()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            objetivoActual.position,
            velocidad * Time.deltaTime
        );

        anim.SetBool("walk", true);

        if (Vector2.Distance(transform.position, objetivoActual.position) < 0.1f)
        {
            objetivoActual = objetivoActual == puntoA ? puntoB : puntoA;

            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    IEnumerator CicloBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreAtaques);

            int ataque = Random.Range(0, 2);

            atacando = true;
            anim.SetBool("walk", false);

            if (ataque == 0)
                yield return StartCoroutine(AtaqueGrito());
            else
                yield return StartCoroutine(AtaqueGolpe());

            atacando = false;
        }
    }

    IEnumerator AtaqueGrito()
    {
        anim.SetTrigger("grito");
        puedeSerGolpeado = true;

        yield return new WaitForSeconds(tiempoGrito);

        puedeSerGolpeado = false;
    }

    IEnumerator AtaqueGolpe()
    {
        anim.SetTrigger("golpe");

        yield return new WaitForSeconds(tiempoGolpe);

        Collider2D hit = Physics2D.OverlapCircle(transform.position, radioGolpe, playerLayer);

        if (hit != null)
        {
            Player p = hit.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(dañoGolpe);
            }
        }
    }

    public void RecibirDaño(int cantidad)
    {
        if (!puedeSerGolpeado) return;

        vida -= cantidad;
        Debug.Log("Boss recibió daño. Vida actual: " + vida);

        if (vida <= 0)
            Morir();
    }

    void Morir()
    {
        Debug.Log("Boss murió");
        anim.SetTrigger("morir");
        Destroy(gameObject, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioGolpe);
    }
}
