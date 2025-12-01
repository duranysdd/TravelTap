using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class VictoryCinematic : MonoBehaviour
{
    public CanvasGroup panel;         // El panel principal
    public TextMeshProUGUI message;   // Tu texto dentro del panel

    private void Awake()
    {
        // El panel inicia invisible
        if(panel != null)
            panel.alpha = 0f;

        // El texto inicia invisible
        if(message != null)
            message.alpha = 0f;
    }

    public void PlayVictory()
    {
        StartCoroutine(PlayCinematic());
    }

    IEnumerator PlayCinematic()
    {
        // Fade del panel
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            panel.alpha = t;
            yield return null;
        }

        // Aparece el texto luego del panel
        float t2 = 0;
        while(t2 < 1)
        {
            t2 += Time.deltaTime;
            message.alpha = t2;
            yield return null;
        }
    }
}
