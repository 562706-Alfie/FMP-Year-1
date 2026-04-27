using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Script does NOT need to go on each button, only the canvas(or anything really)
    public bool isMusicPlayer;

    public void Start()
    {
        
        if (isMusicPlayer == true)
        {
            FindFirstObjectByType<AudioManager>().Play("MainTheme");
            isMusicPlayer = false;
        }

    }

    public void ButtonMethod()
    {
        FindFirstObjectByType<AudioManager>().Play("MenuSelect");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelManager.instance.playerHealth = 10;
    }

    public void PlayMethod()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void ReturntoMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ChangeMusicVolume(float volume)
    {
        AudioManager.instance.musicVolume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        AudioManager.instance.sfxVolume = volume;
    }

    public void PlaySelect()
    {
        AudioManager.instance.Play("MenuSelect");
    }

    public void PlaySelectBack()
    {
        AudioManager.instance.Play("MenuBack");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}