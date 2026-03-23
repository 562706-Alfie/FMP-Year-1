using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        FindFirstObjectByType<AudioManager>().Play("MenuBackgroundMusic");
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    void Update()
    {
        AudioManager.instance.ChangeAudioSourceVolume("MenuBackgroundMusic", AudioManager.instance.musicVolume);
        AudioManager.instance.ChangeAudioSourceVolume("MenuHover", AudioManager.instance.sfxVolume);
        AudioManager.instance.ChangeAudioSourceVolume("MenuSelect", AudioManager.instance.sfxVolume);
        AudioManager.instance.ChangeAudioSourceVolume("PlayerHit", AudioManager.instance.sfxVolume);
        AudioManager.instance.ChangeAudioSourceVolume("PlayerDeath", AudioManager.instance.sfxVolume);

    }
}
