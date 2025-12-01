using UnityEngine;
using System.Collections;

public class LogoFade : MonoBehaviour
{ 
    public CanvasGroup[] images; // Arrastra aquí tus 2 imágenes
    public float fadeDuration = 1f;

    private void Awake()
    {
        foreach (CanvasGroup cg in images)
        {
            if (cg == null) continue;
            cg.alpha = 0f; // invisibles al inicio
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(0f, 1f, t / fadeDuration);

            foreach (CanvasGroup cg in images)
            {
                if (cg != null)
                    cg.alpha = a;
            }

            yield return null;
        }

        foreach (CanvasGroup cg in images)
        {
            if (cg != null)
                cg.alpha = 1f;
        }
    }
}