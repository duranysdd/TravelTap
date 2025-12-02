using System.Collections;
using UnityEngine;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    public static VictoryScreen instance;

    [Header("Fondo de pantalla")]
    public GameObject background;

    [Header("Textos de Victoria")]
    public TMP_Text victoryText;
    public TMP_Text creditsText;

    [Header("Logos")]
    public GameObject logo1;
    public GameObject logo2;

    [Header("Movimiento de Créditos")]
    public float scrollSpeed = 30f;
    public float startDelay = 5f;
    public float scrollDistance = 2000f;

    private Vector3 startPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if (creditsText != null)
            startPos = creditsText.rectTransform.anchoredPosition;

        // OCULTAR TODO AL INICIO
        if (background != null) background.SetActive(false);
        if (victoryText != null) victoryText.gameObject.SetActive(false);
        if (creditsText != null) creditsText.gameObject.SetActive(false);
        if (logo1 != null) logo1.SetActive(false);
        if (logo2 != null) logo2.SetActive(false);
    }

    public void ShowVictory()
    {
        StartCoroutine(ShowSequence());
    }

    private IEnumerator ShowSequence()
    {
        // ACTIVAR FONDO
        if (background != null) background.SetActive(true);

        // VICTORIA
        victoryText.gameObject.SetActive(true);

        yield return new WaitForSeconds(15f);

        // CREDITOS
        victoryText.gameObject.SetActive(false);
        creditsText.gameObject.SetActive(true);

        // LOGOS
        if (logo1 != null) logo1.SetActive(true);
        if (logo2 != null) logo2.SetActive(true);

        yield return new WaitForSeconds(startDelay);

        // INICIAR SCROLL
        yield return StartCoroutine(ScrollCredits());
    }

    private IEnumerator ScrollCredits()
    {
        float moved = 0f;

        while (moved < scrollDistance)
        {
            float step = scrollSpeed * Time.deltaTime;
            creditsText.rectTransform.anchoredPosition += new Vector2(0, step);
            moved += step;
            yield return null;
        }
    }
}
