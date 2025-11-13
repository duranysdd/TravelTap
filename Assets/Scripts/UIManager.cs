using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Score")]
    public TextMeshProUGUI scoreText;

    [Header("Hearts UI")]
    public Image[] corazones; //Esta madre es donde se pone el maximo de corazones
    public Sprite spriteLleno;
    public Sprite spriteVacio;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScore();
        UpdateHearts();
    }

    // Aca se ceuntan los items
    public void UpdateScore()
    {
        if (scoreText == null || GameManager.instance == null) return;
        scoreText.text = $"{GameManager.instance.coleccionables}/{GameManager.instance.coleccionablesNecesarios}";
    }

    // Aca se cuentan las vidas
    public void UpdateHearts()
    {
        if (GameManager.instance == null || corazones == null) return;

        int maxVidas = GameManager.instance.maxVidas;
        int vidasActuales = GameManager.instance.vidasActuales;

        // Corazones según maximo de vidas
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < maxVidas)
            {
                corazones[i].gameObject.SetActive(true);

                // lleno o vacío según vidas actuales
                if (i < vidasActuales)
                    corazones[i].sprite = spriteLleno;
                else
                    corazones[i].sprite = spriteVacio;
            }
            else
            {
                // si la UI tiene más ranuras que el max actual se acumulan 
                corazones[i].gameObject.SetActive(false);
            }
        }
    }
}
