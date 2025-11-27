using UnityEngine;
using System.Collections;

public class VictoryScreen : MonoBehaviour
{
    public static VictoryScreen instance;

    public GameObject cinematic;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        cinematic.SetActive(false);
    }

    public void ShowVictory()
    {
        cinematic.SetActive(true);
        VictoryCinematic cinematicScript = cinematic.GetComponent<VictoryCinematic>();
        if (cinematicScript != null)
        {
            StartCoroutine(cinematicScript.PlayCinematic());
        }
    }
}
