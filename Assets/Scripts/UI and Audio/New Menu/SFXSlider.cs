using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString("0.000");
            PlayerPrefs.SetFloat("sfxVolume", AudioManager.instance.sfxVolume);
        });
    }

}
