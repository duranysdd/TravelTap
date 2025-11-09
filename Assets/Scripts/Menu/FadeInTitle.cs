using UnityEngine;
using UnityEngine.UI;

public class FadeInTitulo : MonoBehaviour
{
    public float duracion = 1.5f;
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        Color c = img.color;
        c.a = 0;
        img.color = c;
        StartCoroutine(FadeIn());
    }

    System.Collections.IEnumerator FadeIn()
    {
        float t = 0;
        while (t < duracion)
        {
            t += Time.deltaTime;
            Color c = img.color;
            c.a = Mathf.Lerp(0, 1, t / duracion);
            img.color = c;
            yield return null;
        }
    }
}
