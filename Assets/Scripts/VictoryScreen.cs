using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public static VictoryScreen instance;

    public GameObject cinematic;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        cinematic.SetActive(false);
    }

    public void ShowVictory()
    {
        cinematic.SetActive(true);
    }
}
