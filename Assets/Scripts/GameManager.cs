using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Progreso general")]
    public int currentLevel = 1;
    public int collectiblesCollected = 0;
    public int totalCollectibles = 20;

    [Header("Vidas del jugador")]
    public int maxHealth = 3;
    public int currentHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void AddCollectible()
    {
        collectiblesCollected++;
        Debug.Log($"Coleccionables: {collectiblesCollected}/{totalCollectibles}");

        if (collectiblesCollected >= totalCollectibles)
        {
            PlayerCompletedLevelWithAllItems();
        }
    }

    private void PlayerCompletedLevelWithAllItems()
    {
        Debug.Log("🎉 Nivel completado con todos los coleccionables. +1 vida máxima!");
        maxHealth++;
        currentHealth = maxHealth;
        collectiblesCollected = 0;
        GoToNextLevel();
    }

    public void PlayerCompletedLevelWithoutAllItems()
    {
        Debug.Log("Nivel completado, pero sin todos los ítems. Conserva vidas actuales.");
        collectiblesCollected = 0;
        GoToNextLevel();
    }

    private void GoToNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            currentLevel++;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("🎮 ¡Has completado todos los niveles!");
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ResetProgress()
    {
        currentLevel = 1;
        maxHealth = 3;
        currentHealth = maxHealth;
        collectiblesCollected = 0;
    }
}
