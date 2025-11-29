using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuMusic;

    private static MusicManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayMenuMusic();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 🔥 aquí decides qué escenas son “MENÚ”
        if (scene.name == "MenuInicial" || scene.name == "MenuOpciones" || scene.name == "MenuNiveles")
        {
            PlayMenuMusic();
        }
        else
        {
            audioSource.Stop(); // 🔇 NO suena en niveles
        }
    }

    private void PlayMenuMusic()
    {
        if (audioSource.clip != menuMusic)
        {
            audioSource.clip = menuMusic;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.volume = AudioListener.volume;
            audioSource.Play();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}
