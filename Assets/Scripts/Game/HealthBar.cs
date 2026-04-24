using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Slider slider;
    public int test;
  
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
