using UnityEngine;
using UnityEngine.UI;

public class LogicaBrillo : MonoBehaviour
{
    public Slider slider;
    public Image panelBrillo;

    private float sliderValue;

    void Start()
    {
        // Carga el valor guardado o usa 0.5 como valor por defecto
        sliderValue = PlayerPrefs.GetFloat("brillo", 0.5f);
        slider.value = sliderValue;

        // Aplica el brillo inicial
        ActualizarBrillo(sliderValue);

        //   evento del slider
        slider.onValueChanged.AddListener(ChangeSlider);
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("brillo", sliderValue);
        ActualizarBrillo(sliderValue);
    }

    private void ActualizarBrillo(float valor)
    {
        // Solo cambia el canal alfa del panel pero manteniendo el
        Color c = panelBrillo.color;
        c.a = valor;
        panelBrillo.color = c;
    }
}
