using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    float musicVol, sfxVol, oldMusicSlider;

    public Slider musicSlider;
    public Slider sfxSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //set up sfx and music sliders
        //start playing the menu music

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        print("musicvol=" + PlayerPrefs.GetFloat("musicVolume"));

        AudioManager.instance.Play("NewMenuBackground2");


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
