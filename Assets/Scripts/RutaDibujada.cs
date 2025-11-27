using UnityEngine;

public class RutaDibujada : MonoBehaviour
{
   public Transform[] waypoints;     // Puntos del camino en orden
    public GameObject dotPrefab;      // Tu prefab de migaja
    public float spacing = 0.5f;      // Distancia entre puntos

    void Start()
    {
        if (waypoints.Length < 2)
        {
            Debug.LogError("Necesitas al menos 2 waypoints para dibujar una ruta.");
            return;
        }

        DibujarRuta();
    }

    void DibujarRuta()
    {
        // Recorre cada par de waypoints
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Transform a = waypoints[i];
            Transform b = waypoints[i + 1];

            float distance = Vector2.Distance(a.position, b.position);
            int count = Mathf.CeilToInt(distance / spacing);

            for (int j = 0; j <= count; j++)
            {
                Vector3 pos = Vector3.Lerp(a.position, b.position, j / (float)count);

                GameObject dot = Instantiate(dotPrefab, pos, Quaternion.identity);

                // Asegurar que siempre esté visible sobre todo
                SpriteRenderer sr = dot.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sortingLayerName = "Guide";
                    sr.sortingOrder = 999;
                }

                dot.transform.parent = transform; // Mantener orden
            }
        }
    }
}