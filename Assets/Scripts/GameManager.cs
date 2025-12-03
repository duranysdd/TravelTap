using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool tieneEspada = false;

    [Header("UI Mensajes")]
    public TextMeshProUGUI mensajeUI;

    public int coleccionables = 0; 
    public int coleccionablesNecesarios = 20; 

    public int maxVidas = 3; 
    public int vidasActuales; 
    public int maxVidasLimit = 5; 

    private Player player;  // ← REFERENCIA AL PLAYER

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            vidasActuales = maxVidas;
            coleccionables = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        BuscarPlayer();
    }

    private void OnLevelWasLoaded(int level)
    {
        BuscarPlayer();
    }

    private void BuscarPlayer()
    {
        player = FindObjectOfType<Player>();
    }

    public void MostrarMensaje(string texto, float tiempo = 3f)
    {
        if (mensajeUI != null)
            StartCoroutine(MostrarMensajeRoutine(texto, tiempo));
    }

    private IEnumerator MostrarMensajeRoutine(string texto, float tiempo)
    {
        mensajeUI.text = texto;
        mensajeUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(tiempo);

        mensajeUI.gameObject.SetActive(false);
    }

    public void AgregarColeccionable(int amount = 1)
    {
        coleccionables += amount;

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

        return vidasActuales <= 0;
    }

    public void Curar(int amount)
    {
        vidasActuales += amount;
        vidasActuales = Mathf.Clamp(vidasActuales, 0, maxVidas);

        if (UIManager.instance != null)
            UIManager.instance.UpdateHearts();
    }

    public void ResetearColeccionables()
    {
        coleccionables = 0;

        if (UIManager.instance != null)
            UIManager.instance.UpdateScore();
    }

    public void CompletarNivel()
    {
        if (coleccionables >= coleccionablesNecesarios)
        {
            coleccionables = 0;
            AumentarVidaMaxima();
        }
        else
        {
            coleccionables = 0;
            if (UIManager.instance != null)
                UIManager.instance.UpdateScore();
        }

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScene);
        else
            SceneManager.LoadScene("MainMenu");
    }

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

    public void RespawnPlayer()
    {
        if (player == null)
        {
            BuscarPlayer();
            if (player == null) return;
        }

        // Reposicionar en el checkpoint
        player.transform.position = player.checkPoint;

        // Restaurar vidas
        vidasActuales = maxVidas;

        if (UIManager.instance != null)
            UIManager.instance.UpdateHearts();
    }
}
