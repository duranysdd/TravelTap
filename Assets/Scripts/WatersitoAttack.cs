using UnityEngine;

public class WatersitoAttack : MonoBehaviour
{
    [Header("Arco Sagrado")]
    public GameObject arcoPrefab;  
    public Transform firePoint;

    [Header("Habilidad")]
    public bool habilidadDesbloqueada = false;

    public float shootInterval = 0.6f;
    private float shootTimer = 0f;

    void Update()
    {
        if (!habilidadDesbloqueada) return;   // Si no está desbloqueada, no ataca

        shootTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F) && shootTimer >= shootInterval)
        {
            DispararArco();
            shootTimer = 0f;
        }
    }

  void DispararArco()
{
    if (arcoPrefab == null || firePoint == null) return;

    GameObject arco = Instantiate(arcoPrefab, firePoint.position, firePoint.rotation);

    // Detecta la dirección en la que mira Watersito
    float direccion = transform.localScale.x;

    // Ajusta la escala del proyectil
    arco.transform.localScale = new Vector3(direccion * Mathf.Abs(arco.transform.localScale.x),
                                            arco.transform.localScale.y,
                                            arco.transform.localScale.z);
}
}
