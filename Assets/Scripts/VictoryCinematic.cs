using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryCinematic : MonoBehaviour
{
    public CanvasGroup panel;     // Panel negro
    public Text message;          // Texto del mensaje
    public GameObject[] logos;    // Logos que aparecerán

    public float fadeTime = 1f;
    public float waitTime = 1.5f;
    public float slideDistance = 50f; // Desplazamiento del texto desde arriba

    private void Awake()
    {
        panel.alpha = 0f;
        panel.blocksRaycasts = true; // Bloquea input si quieres
        message.gameObject.SetActive(false);

        foreach (GameObject logo in logos)
            logo.SetActive(false);
    }

    public IEnumerator PlayCinematic()
    {
        gameObject.SetActive(true);

        // Mostrar texto
        message.gameObject.SetActive(true);

        // Posición inicial del texto
        RectTransform rt = message.GetComponent<RectTransform>();
        Vector3 endPos = rt.localPosition;
        Vector3 startPos = endPos + new Vector3(0, slideDistance, 0);
        rt.localPosition = startPos;

        // Fade-in del panel y deslizar el texto
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeTime);
            panel.alpha = alpha;
            rt.localPosition = Vector3.Lerp(startPos, endPos, t / fadeTime);
            yield return null;
        }
        panel.alpha = 1f;
        rt.localPosition = endPos;

        // Mostrar logos con fade
        foreach (GameObject logo in logos)
        {
            logo.SetActive(true);
            LogoFade fade = logo.GetComponent<LogoFade>();
            if (fade != null)
                fade.FadeIn();
        }

        // Esperar tiempo de lectura
        yield return new WaitForSecondsRealtime(waitTime);

        // Fade-out del panel y deslizar texto hacia abajo
        t = 0f;
        Vector3 textPos = rt.localPosition;
        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            panel.alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
            rt.localPosition = Vector3.Lerp(textPos, textPos - new Vector3(0, slideDistance / 2, 0), t / fadeTime);
            yield return null;
        }

        panel.alpha = 0f;
        gameObject.SetActive(false);
    }
}
