using UnityEngine;

public class Elevador : MonoBehaviour
{
    [Header("Puntos de Movimiento")]
    public Transform pointA;
    public Transform pointB;

    [Header("Ajustes")]
    public float moveSpeed = 2f;
    public float waitTime = 2f;

    private Vector3 targetPosition;
    private bool isWaiting = false;

    private void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("No hay puntos asignados al elevador");
            enabled = false;
            return;
        }

        // Elegir punto inicial
        float distA = Vector3.Distance(transform.position, pointA.position);
        float distB = Vector3.Distance(transform.position, pointB.position);
        targetPosition = (distA < distB) ? pointB.position : pointA.position;
    }

    private void FixedUpdate()
    {
        if (isWaiting) return;

        // Mover hacia el objetivo sin pasarse del punto
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.fixedDeltaTime
        );

        // Detectar llegada exacta
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            StartCoroutine(WaitAndSwitch());
        }
    }

    private System.Collections.IEnumerator WaitAndSwitch()
    {
        isWaiting = true;

        yield return new WaitForSeconds(waitTime);

        // Cambiar destino después de esperar
        targetPosition = (targetPosition == pointB.position)
            ? pointA.position
            : pointB.position;

        isWaiting = false;
    }

    // --------------------------
    // PEGAR PLAYER A LA PLATAFORMA
    // --------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
