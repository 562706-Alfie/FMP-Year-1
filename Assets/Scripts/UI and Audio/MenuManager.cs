using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    float musicVol, sfxVol, oldMusicSlider;
    public int difficulty;

    public Slider musicSlider;
    public Slider sfxSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficulty = PlayerPrefs.GetInt("Difficulty");
        }
        else
        {
            PlayerPrefs.SetInt("Difficulty", 10);
        }
    }

    void Start()
    {
        //set up sfx and music sliders
        //start playing the menu music

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void Update()
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }

    public void MusicDisable()
    {
        oldMusicSlider = musicSlider.value;
        musicSlider.value = 0.0001f;
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void MusicEnable()
    {
        musicSlider.value = oldMusicSlider;
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}
