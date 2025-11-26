using System.Collections;
using UnityEngine;

public class VictoryTitleAnimation : MonoBehaviour
{
    public float appearTime = 1f;

    private CanvasGroup cg;
    private Vector3 startScale;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        startScale = transform.localScale;

        cg.alpha = 0;
        transform.localScale = Vector3.one * 0.5f;
    }

    void OnEnable()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float t = 0;

        while (t < appearTime)
        {
            t += Time.unscaledDeltaTime;

            cg.alpha = Mathf.Lerp(0, 1, t / appearTime);
            transform.localScale = Vector3.Lerp(
                Vector3.one * 0.5f,
                startScale,
                t / appearTime
            );

            yield return null;
        }
    }
}
