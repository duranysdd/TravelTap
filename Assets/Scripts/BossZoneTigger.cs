using System.Collections;
using UnityEngine;

public class BossZoneTigger : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject backgroundNormal;
    public GameObject backgroundBoss;
    public GameObject boss;
    public GameObject barrier; // 🚧 para bloquear el paso
    public CanvasGroup blackScreen; // si tienes el efecto de pantalla negra

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
        // 1️⃣ Fundido a negro (si tienes pantalla negra)
        if (blackScreen != null)
            yield return StartCoroutine(FadeToBlack());

        // 2️⃣ Cambiar el fondo
        backgroundNormal.SetActive(false);
        backgroundBoss.SetActive(true);

        // 3️⃣ Activar el boss
        boss.SetActive(true);

        // 4️⃣ Activar barrera (para que el jugador no regrese)
        barrier.SetActive(true);

        // 5️⃣ Espera un poco (oscuro)
        yield return new WaitForSeconds(waitInDark);

        // 6️⃣ Regresar visibilidad
        if (blackScreen != null)
            yield return StartCoroutine(FadeFromBlack());

        // 7️⃣ Empezar el ataque del boss (si tiene script)
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