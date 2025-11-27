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

    [Header("Habilidad del Arco")]
    public WatersitoAttack watersitoAttack;

    [Header("Música")]
    public AudioSource musicSource;     // AudioSource principal
    public AudioClip normalMusic;       // música normal
    public AudioClip bossMusic;         // música del boss

    // ← Aquí inicializamos el boss apagado correctamente
    void Awake()
    {
        if (boss != null)
        {
            // Mantener GameObject activo para que no se pierda el sprite
            boss.SetActive(true);

            // Ocultar visualmente el boss
            SpriteRenderer sr = boss.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = false;

            // Desactivar scripts y colliders hasta que empiece la pelea
            var controller = boss.GetComponent<BossWaterController>();
            if (controller != null)
                controller.enabled = false;

            var collider = boss.GetComponent<Collider2D>();
            if (collider != null)
                collider.enabled = false;
        }

        if (barrier != null)
            barrier.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !bossStarted)
        {
            bossStarted = true;

            // Cambiar música a la del boss
            if (musicSource != null && bossMusic != null)
            {
                musicSource.Stop();
                musicSource.clip = bossMusic;
                musicSource.Play();
            }

            StartCoroutine(StartBossSequence());
        }
    }

    private IEnumerator StartBossSequence()
    {
        if (blackScreen != null)
            yield return StartCoroutine(FadeToBlack());

        if (backgroundNormal != null) backgroundNormal.SetActive(false);
        if (backgroundBoss != null) backgroundBoss.SetActive(true);

        // Activar el boss visualmente y scripts
        if (boss != null)
        {
            SpriteRenderer sr = boss.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.enabled = true;

            var controller = boss.GetComponent<BossWaterController>();
            if (controller != null)
                controller.enabled = true;

            var collider = boss.GetComponent<Collider2D>();
            if (collider != null)
                collider.enabled = true;
        }

        if (barrier != null) barrier.SetActive(true);

        StartBossFight();

        yield return new WaitForSeconds(waitInDark);

        if (blackScreen != null)
            yield return StartCoroutine(FadeFromBlack());

        // Iniciar ataque del boss
        if (boss != null)
        {
            var controller = boss.GetComponent<BossWaterController>();
            if (controller != null)
                controller.StartAttacking();
        }
    }

    private IEnumerator FadeToBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            if (blackScreen != null)
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
            if (blackScreen != null)
                blackScreen.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }
    }

    void StartBossFight()
    {
        if (watersitoAttack != null)
            watersitoAttack.habilidadDesbloqueada = true;
    }
}
