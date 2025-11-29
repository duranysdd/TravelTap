using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;

    void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("VolumenAudio", 1f);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }
    void Start()
{
    AudioListener.volume = PlayerPrefs.GetFloat("VolumenAudio", 1f);
}


    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("VolumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute()
    {
        imagenMute.enabled = slider.value == 0;
    }
    
}
