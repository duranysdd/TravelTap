using UnityEngine;
using System.Collections.Generic;

 //ya no se ocupa
public class AdvancedMazeGuide : MonoBehaviour
{
    public Transform player;
    public Transform boss;

    [Header("Waypoints en orden")]
    public List<Transform> waypoints;

    private int currentIndex = 0;
    public float smoothRotation = 10f;

    void Update()
    {
        if (currentIndex >= waypoints.Count) return;

        Transform target = waypoints[currentIndex];

        // Dirección hacia el siguiente waypoint
        Vector2 dir = target.position - player.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        // Rotación suave
        Quaternion targetRot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, smoothRotation * Time.deltaTime);

        // Si el jugador llega al waypoint, pasar al siguiente
        if (Vector2.Distance(player.position, target.position) < 1f)
            currentIndex++;
    }
}
