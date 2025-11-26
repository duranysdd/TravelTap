using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryCinematic : MonoBehaviour
{
    public CanvasGroup panel; // Panel negro
    public Text message;      // Texto del mensaje

    public float fadeTime = 1f;
    public float waitTime = 1.5f;

    public IEnumerator PlayCinematic()
    {
        gameObject.SetActive(true);
        panel.alpha = 1;
        message.canvasRenderer.SetAlpha(0);

        // Fade in del texto
        message.CrossFadeAlpha(1, fadeTime, false);

        yield return new WaitForSecondsRealtime(waitTime);

        // Fade out del panel completo
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            panel.alpha = Mathf.Lerp(1, 0, t / fadeTime);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
