using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Referencias UI")]
    public TextMeshProUGUI scoreText;
    public Transform heartsPanel;
    public GameObject heartPrefab; // usaremos esto para crear corazones dinámicamente

    private Player player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        UpdateScore(player.collectibles);
        UpdateHearts();
    }

    public void UpdateScore(int newScore)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + newScore.ToString();
    }

    public void UpdateHearts()
    {
        if (player == null || heartsPanel == null) return;

        // Limpiar corazones antiguo
        foreach (Transform child in heartsPanel)
        {
            Destroy(child.gameObject);
        }

        // Crear nuevos corazones según la vida máxima
        for (int i = 0; i < player.maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsPanel);
            Image img = heart.GetComponent<Image>();

            // Si la vida actual es menor, hacerlo más transparente
            if (i >= player.currentHealth)
            {
                Color c = img.color;
                c.a = 0.3f; // transparente
                img.color = c;
            }
        }
    }

    public void UpdatePlayerHealth()
    {
        UpdateHearts();
    }
   

}
