using System.Collections;
using UnityEngine;

public class BossZoneTigger : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject backgroundNormal;
    public GameObject backgroundBoss;
    public GameObject boss;
    public GameObject barrier; 
    public CanvasGroup blackScreen; 

    [Header("Configuración")]
    public float fadeDuration = 1.5f;
    public float waitInDark = 1f;

    private bool bossStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !bossStarted)
        {
            bossStarted = true;
            StartCoroutine(StartBossSequence());
        }
    }

    private IEnumerator StartBossSequence()
    {
        
        if (blackScreen != null)
            yield return StartCoroutine(FadeToBlack());

        
        backgroundNormal.SetActive(false);
        backgroundBoss.SetActive(true);

        
        boss.SetActive(true);

        
        barrier.SetActive(true);

        
        yield return new WaitForSeconds(waitInDark);

        
        if (blackScreen != null)
            yield return StartCoroutine(FadeFromBlack());

        
        var controller = boss.GetComponent<BossWaterController>();
        if (controller != null)
            controller.StartAttacking();
    }

    private IEnumerator FadeToBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeFromBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }
    }
}

