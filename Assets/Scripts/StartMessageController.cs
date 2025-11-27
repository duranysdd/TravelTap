using UnityEngine;
using TMPro;
using UnityEngine.UI;  // Necesario para Image
using System.Collections;

public class StartMessageController : MonoBehaviour
{
    public TMP_Text messageText;
    public Image backgroundPanel;       // Nuevo: referencia al panel de fondo
    public float showDuration = 2f;     
    public float fadeDuration = 1.5f;   

    void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        // Asegurar que texto y fondo inician visibles
        SetAlpha(messageText, 1f);
        SetAlpha(backgroundPanel, 0.5f);  // fondo semi-opaco
        messageText.gameObject.SetActive(true);
        backgroundPanel.gameObject.SetActive(true);

        // Esperar antes del fade
        yield return new WaitForSeconds(showDuration);

        // Fade out
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alphaText = Mathf.Lerp(1f, 0f, t / fadeDuration);
            float alphaBg = Mathf.Lerp(0.5f, 0f, t / fadeDuration);

            SetAlpha(messageText, alphaText);
            SetAlpha(backgroundPanel, alphaBg);

            yield return null;
        }

        // Ocultar al final
        messageText.gameObject.SetActive(false);
        backgroundPanel.gameObject.SetActive(false);
    }

    void SetAlpha(TMP_Text text, float alpha)
    {
        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }

    void SetAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
