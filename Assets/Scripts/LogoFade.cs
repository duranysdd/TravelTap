using UnityEngine;
using System.Collections;

public class LogoFade : MonoBehaviour
{
    public float fadeDuration = 1f;
    private CanvasGroup cg;

    void Awake()
    {
        // Obtener CanvasGroup o agregarlo si no existe
        cg = GetComponent<CanvasGroup>();
        if (cg == null)
            cg = gameObject.AddComponent<CanvasGroup>();

        cg.alpha = 0f; // Empieza invisible
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
            cg.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        cg.alpha = 1f;
    }
}
