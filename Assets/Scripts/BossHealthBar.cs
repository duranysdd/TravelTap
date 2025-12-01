using UnityEngine;
using UnityEngine.UI;


//YA NO SE OCUPA
public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public BossHealth boss;

    void Start()
    {
        slider.maxValue = boss.maxHealth;
        slider.value = boss.maxHealth;
    }

    void Update()
    {
        slider.value = boss.CurrentHealth();
    }
}
