using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coleccionables = 0; 
    public int coleccionablesNecesarios = 20; 

    public int maxVidas = 3; 
    public int vidasActuales; 
    public int maxVidasLimit = 5; 

    private void Awake()
    {
        // Singleton persistente
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Inicialización sólo la primera vez que se crea el GameManager
            vidasActuales = maxVidas;
            coleccionables = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AgregarColeccionable(int amount = 1)
    {
        coleccionables += amount;

        // Si se alcanzan los coleccionables necesarios, aumenta vida máxima (si aplica)
        if (coleccionables >= coleccionablesNecesarios)
        {
            coleccionables = 0;
            AumentarVidaMaxima();
        }

        if (UIManager.instance != null)
            UIManager.instance.UpdateScore();
    }
    private void AumentarVidaMaxima()
    {
        if (maxVidas < maxVidasLimit)
        {
            maxVidas++;
            vidasActuales = maxVidas;
        }

        if (UIManager.instance != null)
            UIManager.instance.UpdateHearts();
    }
    public bool TomarDaño(int amount)
    {
        vidasActuales -= amount;
        vidasActuales = Mathf.Clamp(vidasActuales, 0, maxVidas);

        if (UIManager.instance != null)
            UIManager.instance.UpdateHearts();

        // Retorna true si el jugador murió
        return vidasActuales <= 0;
    }

    // Contador de Vidas
    public void Curar(int amount)
    {
        vidasActuales += amount;
        vidasActuales = Mathf.Clamp(vidasActuales, 0, maxVidas);

        if (UIManager.instance != null)
            UIManager.instance.UpdateHearts();
    }

    // Contador de Items
    public void ResetearColeccionables()
    {
        coleccionables = 0;

        if (UIManager.instance != null)
            UIManager.instance.UpdateScore();
    }

    // usa coleccionables actuales para decidir
    public void CompletarNivel()
    {
        if (coleccionables >= coleccionablesNecesarios)
        {
            // Ya no lo vamos a manejar por AddCollectible (por seguridac)
            coleccionables = 0;
            AumentarVidaMaxima();
        }
        else
        {
            // Esta madre es por si no obtuvo los items
            coleccionables = 0;
            if (UIManager.instance != null)
                UIManager.instance.UpdateScore();
        }

        // Avanza la  escena
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Reinicia todo el progreso 
    public void ResetProgress()
    {
        coleccionables = 0;
        maxVidas = 3;
        vidasActuales = maxVidas;

        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateScore();
            UIManager.instance.UpdateHearts();
        }
    }
}
