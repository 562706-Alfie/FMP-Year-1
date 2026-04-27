using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameManager gameManager;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void RegenerateHealth()
    {
        slider.value += gameManager.rateHealthRegenerate;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
