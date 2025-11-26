using UnityEngine;
using TMPro;

public class QualitySettingsMenu : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;

    void Start()
    {
        // Cargar lista de calidades disponibles
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new System.Collections.Generic.List<string>(QualitySettings.names));

        // Seleccionar la calidad actual
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        Debug.Log("Calidad cambiada a: " + QualitySettings.names[index]);
    }
}
